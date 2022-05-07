using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Api.Extensions;
using Api.Settings;
using Core.Auth;
using Core.Common;
using Core.Helpers;
using Core.Interfaces;
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


    public AuthController(IUnitOfWork unitOfWork, IConfiguration config)
    {
        _unitOfWork = unitOfWork;
        _config = config;
    }

    [HttpGet("check")]
    [ProducesResponseType(typeof(AuthCheckResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Check([Required][FromQuery] AuthCheckRequest model)
    {

        var phone = _unitOfWork.Auth.PreparePhoneNumber(model.Mobile);


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

        var phone = _unitOfWork.Auth.PreparePhoneNumber(model.Mobile);

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
    public async Task<ActionResult<AuthInfo>> GetAuthInfo([FromServices] UserAuthContext authContext)
    {
        var user = authContext.GetAuthUser();
        var MySqlUser = await _unitOfWork.User.GetUserByAuthId(user.Id, true);

        if (MySqlUser is null)
            return NotFound(BasicResponse.ResourceNotFound);



        AuthInfo? AuthInfo = await _unitOfWork.Auth.AuthInfo(MySqlUser);

        if (AuthInfo is null)
            return NotFound(ErrorCode.ResourceDoesNotExist);

        return Ok(AuthInfo);


    }
}