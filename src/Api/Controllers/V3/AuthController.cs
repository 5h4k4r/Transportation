using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Api.Extensions;
using Api.Settings;
using Core.Interfaces;
using Core.Models.Authentication;
using Core.Models.Base;
using Core.Models.Common;
using Core.Models.Requests;
using Infra.Authentication;
using Infra.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Department = Core.Models.Authentication.Department;

namespace Api.Controllers.V3;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[Route("v3/auth")]
public class AuthController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _config;
    


    public AuthController(IUnitOfWork unitOfWork, IConfiguration config)
    {
        _unitOfWork = unitOfWork;
        _config = config;
  
    }

    [HttpGet("check")]
    [ProducesResponseType(typeof(AuthCheckResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Check([Required] [FromQuery] AuthCheckRequest model)
    {
        var phone = PreparePhoneNumber(model.Mobile!);

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
        // TODO: mobile is nullable
        if (model.Mobile != null)
        {
            var phone = PreparePhoneNumber(model.Mobile);

            var user = await _unitOfWork.User.GetUserByPhone(phone);

            if (user is null)
                return NotFound(BasicResponse.ResourceNotFound);

            user.AuthId = model.AuthId;
        }

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
        var mySqlUser = await _unitOfWork.User.GetUserByAuthId(user.Id, true);

        if (mySqlUser is null)
            return NotFound(BasicResponse.ResourceNotFound);

        var areaInfo = await _unitOfWork.AreaInfos.GetAreaInfoByUser(mySqlUser);

        if (areaInfo is null)
            return NotFound(BasicResponse.ResourceDoesNotExist(nameof(areaInfo)));

        RoleUserDto? roleUserWithDepartment = null;
        var k = mySqlUser.RoleUsers.OrderBy(x => x.RoleId).ToList();
        foreach (var roleUser in k)
        {
            var areaDepartment = await _unitOfWork.AreaDepartments.GetAreaDepartmentByRoleUserId(roleUser.Id);
            if (areaDepartment?.Department is not null)
                roleUserWithDepartment = roleUser;
        }

        if (roleUserWithDepartment is null)
            return null;

        var department = await _unitOfWork.Departments.GetDepartmentById(1);

        if (department is null)
            return null;

        var currentRole = await _unitOfWork.Roles.GetRoleById(1);

        AuthInfo authInfoResponse = new()
        {
            BirthDate = mySqlUser.BirthDate,
            Id = mySqlUser.Id,
            AreaId = areaInfo.Id,
            AuthId = mySqlUser.AuthId,
            MapCenter = new MapCenter(areaInfo.Center),
            Department = new Department
            {
                Id = department.Id,
                Title = department.Title,
                Role = new DepartmentRole
                {
                    Id = currentRole?.Id,
                    Title = currentRole?.Title,
                }
            },
            Mobile = user.Mobile,
            Name = mySqlUser.Name,
            Version = "V3",
            IsAdmin = currentRole?.Id == 2 || currentRole?.Id == 6,
            IsSuperAdmin = currentRole?.Id == 1
        };


        return Ok(authInfoResponse);
    }

    private static string PreparePhoneNumber([Required] string model)
    {
        if (model[0] != '+')
            model = "+" + model;


        return model;
    }
}