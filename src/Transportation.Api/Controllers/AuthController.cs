using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.PaygridApi.Helpers;
using Transportation.Api.Http;
using Transportation.Api.Model;
using Transportation.Api.Models;
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

    private readonly AuthService _authService;
    private readonly ILogger<AuthController> _logger;


    public AuthController(ILogger<AuthController> logger, AuthService authService)
    {
        _logger = logger;
        _authService = authService;
    }

    [Authorize(AuthenticationSchemes = "Basic")]
    [HttpGet("check")]
    public async Task<ActionResult<Language>> Check()
    {

        //     // model.Name;
        //     // model.Mobile;
        //     // model.AuthId;
        //     // model.GenderId;
        //     // model.LanguageId;
        //     // model.BirthDate;
        //     // model.AreaId;
        //     // model.CreatedAt;
        //     // model.UpdatedAt;
        //     // model.DeletedAt;




        return Ok(User.Claims.FirstOrDefault());
    }

    [HttpPost("login")]
    public async Task<ActionResult<Language>> Login()
    {
        return Ok();
    }

    [Authorize]
    [HttpGet("info")]
    public async Task<ActionResult<AuthInfoResponse>> GetAuthInfo([FromServices] UserAuthContext authContext)
    {
        AuthInfoResponse? authInfoResponse = await _authService.AuthInfo(authContext);
        return Ok(authInfoResponse);
        // var user2 = user;
        return Ok(new ApiResponse<AuthInfoResponse>(authInfoResponse, Message: "Done"));


    }
}