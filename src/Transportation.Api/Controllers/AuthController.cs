using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payroll.PaygridApi.Helpers;
using Transportation.Api.Common;
using Transportation.Api.Helpers;
using Transportation.Api.Interfaces;
using Transportation.Api.Models.Common;
using Transportation.Api.Requests;
using Transportation.Api.Responses;
using Transportation.Api.Settings;

namespace Transportation.Api.Controllers;

[Authorize]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[Route("v3/auth")]
public class AuthController : ControllerBase
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AuthController> _logger;
    private readonly IConfiguration config;


    public AuthController(ILogger<AuthController> logger, IUnitOfWork unitOfWork, IConfiguration _config)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        config = _config;
    }

    [HttpPost("check")]
    public async Task<ActionResult<AuthCheckResponse?>> Check([Required] AuthCheckRequest model)
    {

        var user = await _unitOfWork.Auth.Check(model.Mobile);

        if (user is null)
            return NotFound(BasicResponse.ResourceNotFound);


        if (!user.HasRole("superadmin") && !user.HasRole("admin"))
            return Forbid();

        var authServer = config.GetSection(VariableSettings.Config).Get<VariableSettings>();

        AuthCheckResponse authCheckResponse = new()
        {
            AuthUrl = authServer?.AuthServer?.AuthUrl ?? "",
            ServiceId = authServer?.AuthServer?.ServiceId ?? ""
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
    [HttpGet("info")]
    public async Task<ActionResult<AuthInfoResponse>> GetAuthInfo([FromServices] UserAuthContext authContext)
    {
        AuthInfoResponse? authInfoResponse = await _unitOfWork.Auth.AuthInfo(authContext);

        if (authInfoResponse is null)
            return NotFound(ErrorCode.ResourceDoesNotExist);

        return Ok(authInfoResponse);


    }
}