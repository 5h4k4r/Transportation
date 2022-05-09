using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Api.Extensions;
using Api.Settings;
using AutoMapper;
using Core.Auth.Models;
using Core.Common;
using Core.Helpers;
using Core.Interfaces;
using Infra.Entities;
using Infra.Entities.Common;
using Infra.Requests;
using Infra.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Api.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[Route("v3/auth")]
public class AuthController : ControllerBase
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _config;

    private readonly IMapper _mapper;


    public AuthController(IUnitOfWork unitOfWork, IConfiguration config, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _config = config;
        _mapper = mapper;
    }

    [HttpGet("check")]
    [ProducesResponseType(typeof(AuthCheckResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Check([Required][FromQuery] AuthCheckRequest model)
    {

        var phone = PreparePhoneNumber(model.Mobile);

        var user = await _unitOfWork.User.GetUserByPhone(phone, true);

        if (user is null)
            return NotFound(BasicResponse.ResourceNotFound);

        if (!user.HasRole("superadmin") && !user.HasRole("admin"))
            return Forbid();

        var settings = _config.GetSection(SettingsConfig.Config).Get<SettingsConfig>();

        AuthCheckResponse authCheckResponse = new()
        {
            AuthUrl = settings?.AuthServer?.AuthUrl ?? "",
            ServiceId = settings?.AuthServer?.ServiceId ?? ""
        };

        return Ok(authCheckResponse);

    }

    [HttpPost("login")]
    public async Task<ActionResult<object?>> Login(LoginRequest model)
    {

        var phone = PreparePhoneNumber(model.Mobile);

        var user = await _unitOfWork.User.GetUserByPhone(phone);

        if (user is null)
            return NotFound(BasicResponse.ResourceNotFound);

        user.AuthId = model.AuthId;

        await _unitOfWork.Save();

        return NoContent();
    }

    /// <summary>
    /// Gets the current signed in user.
    /// </summary>
    [Authorize]
    [HttpGet("info")]
    public async Task<ActionResult<AuthInfo>?> GetAuthInfo([FromServices] UserAuthContext authContext)
    {
        var user = authContext.GetAuthUser();
        var MySqlUser = await _unitOfWork.User.GetUserByAuthId(user.Id, true);

        if (MySqlUser is null)
            return NotFound(BasicResponse.ResourceNotFound);

        var databaseUser = _mapper.Map<User>(MySqlUser);
        var AreaInfo = await _unitOfWork.AreaInfos.GetAreaInfoByUser(MySqlUser);

        RoleUser? RoleUserWithDepartment = new();
        databaseUser.RoleUsers.OrderBy(x => x.RoleId).ToList().ForEach(async RoleUser =>
        {

            var AreaDepartment = await _unitOfWork.AreaDepartments.GetAreaDepartmentByRoleUserId(RoleUser.Id);
            Console.WriteLine("Area department" + AreaDepartment?.Department);
            if (AreaDepartment?.Department is not null)
                RoleUserWithDepartment = RoleUser;
        });

        if (RoleUserWithDepartment is null)
            return null;

        var Department = await _unitOfWork.Departments.GetDepartmentById(1);

        if (Department is null)
            return null;

        var CurrentRole = await _unitOfWork.Roles.GetRoleById(1);

        AuthInfo authInfoResponse = new()
        {
            BirthDate = MySqlUser?.BirthDate,
            Id = MySqlUser?.Id,
            AreaId = (ulong)AreaInfo?.Id,
            AuthId = MySqlUser?.AuthId,
            MapCenter = new MapCenter(AreaInfo?.Center),
            Department = new Core.Auth.Department
            {
                Id = (ulong)Department.Id,
                Title = Department.Title ?? "",
                Role = new Core.Auth.Role
                {
                    Id = CurrentRole?.Id,
                    Title = CurrentRole?.Title,
                }
            },
            Mobile = user!.Mobile,
            Name = MySqlUser?.Name,
            Version = "V3",
            IsAdmin = CurrentRole?.Id == 2 || CurrentRole?.Id == 6,
            IsSuperAdmin = CurrentRole?.Id == 1
        };


        if (authInfoResponse is null)
            return NotFound(BasicResponse.ResourceNotFound);

        return Ok(authInfoResponse);


    }

    private static string PreparePhoneNumber([Required] string model)
    {
        if (model[0] != '+')
            model = "+" + model;


        return model;
    }
}