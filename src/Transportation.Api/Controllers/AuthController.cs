using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payroll.PaygridApi.Helpers;
using Transportation.Api.Common;
using Transportation.Api.Extensions;
using Transportation.Api.Interfaces;
using Transportation.Api.Models.Common;
using Transportation.Api.Requests;
using Transportation.Api.Responses;
using Transportation.Api.Settings;

namespace Transportation.Api.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[Route("v3/auth")]
public class AuthController : ControllerBase
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AuthController> _logger;
    private readonly IConfiguration _config;


    public AuthController(ILogger<AuthController> logger, IUnitOfWork unitOfWork, IConfiguration config)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _config = config;
    }

    [HttpPost("check")]
    public async Task<ActionResult<AuthCheckResponse?>> Check([Required] AuthCheckRequest model)
    {

        var user = await _unitOfWork.Auth.Check(model.Mobile);

        if (user is null)
            return NotFound(BasicResponse.ResourceNotFound);

        if (!(user.HasRole("superadmin") || user.HasRole("admin")))
            return Forbid();

        var settings = _config.GetSection(VariableSettings.Config).Get<VariableSettings>();

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
        var user = await _unitOfWork.Auth.Login(model);

        if (user is null)
            return NotFound(BasicResponse.ResourceNotFound);

        user.AuthId = model.AuthId;

        _unitOfWork.Save();

        return NoContent();
    }

    /// <summary>
    /// Gets the current signed in user.
    /// </summary>
    [Authorize]
    [HttpGet("info")]
    public async Task<ActionResult<AuthInfoResponse>> GetAuthInfo([FromServices] UserAuthContext authContext)
    {
        AuthInfoResponse? authInfoResponse = await _unitOfWork.Auth.AuthInfo(authContext);

        if (authInfoResponse is null)
            return NotFound(ErrorCode.ResourceDoesNotExist);

        return Ok(authInfoResponse);


    }
}