using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payroll.PaygridApi.Helpers;
using Transportation.Api.Common;
using Transportation.Api.Http;
using Transportation.Api.Model;
using Transportation.Api.Repositories;
using Transportation.Api.Requests;
using Transportation.Api.Responses;
using Transportation.Api.Services;

namespace Transportation.Api.Controllers;

[Authorize]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[Route("v3/auth")]
public class AuthController : ControllerBase
{

    private readonly IAuthRepository _authService;
    private readonly ILogger<AuthController> _logger;


    public AuthController(ILogger<AuthController> logger, IAuthRepository authService)
    {
        _logger = logger;
        _authService = authService;
    }

    // [Authorize(AuthenticationSchemes = "Basic")]
    // [HttpGet("check")]
    // public async Task<ActionResult<Language>> Check()
    // {

    //     //     // model.Name;
    //     //     // model.Mobile;
    //     //     // model.AuthId;
    //     //     // model.GenderId;
    //     //     // model.LanguageId;
    //     //     // model.BirthDate;
    //     //     // model.AreaId;
    //     //     // model.CreatedAt;
    //     //     // model.UpdatedAt;
    //     //     // model.DeletedAt;




    //     return Ok(User.Claims.FirstOrDefault());
    // }
    [Authorize]
    [HttpPost("login")]
    public async Task<ActionResult<object?>> Login(LoginRequest model)
    {
        var result = await _authService.Login(model);

        if (result is ErrorCode.ResourceDoesNotExist)
            return NotFound();

        return Ok(result);
    }

    [Authorize]
    [HttpGet("info")]
    public async Task<ActionResult<AuthInfoResponse>> GetAuthInfo([FromServices] UserAuthContext authContext)
    {
        AuthInfoResponse? authInfoResponse = await _authService.GetAuthInfo(authContext);
        if (authInfoResponse is null)
            return NotFound();

        return Ok(authInfoResponse);


    }
}