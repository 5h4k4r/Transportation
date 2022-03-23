using System.Text.Json;
using BabyCareApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payroll.PaygridApi.Helpers;
using Transportation.Api.Model;

namespace Transportation.Api.Controllers;

[ApiController]
[Route("v1/auth")]
public class AuthController : ControllerBase
{

    private static transportationContext context = new transportationContext();
    private readonly ILogger<AuthController> _logger;

    public record AuthRequest(string Mobile);

    public AuthController(ILogger<AuthController> logger)
    {
        _logger = logger;
    }

    [Authorize(AuthenticationSchemes = "Basic")]
    [HttpGet("check")]
    public async Task<ActionResult<Language>> Check()
    {

        // model.Name;
        // model.Mobile;
        // model.AuthId;
        // model.GenderId;
        // model.LanguageId;
        // model.BirthDate;
        // model.AreaId;
        // model.CreatedAt;
        // model.UpdatedAt;
        // model.DeletedAt;




        return Ok(User.Claims.FirstOrDefault());
    }

    [HttpPost("login")]
    public async Task<ActionResult<Language>> Login()
    {
        return Ok();
    }

    [Authorize(AuthenticationSchemes = "Basic")]
    [HttpGet("info")]
    public async Task<ActionResult<Language>> GetAuthInfo()
    {
        var user = context.Users.Where(user => User.GetAuthId() == user.AuthId);

        if (user is null)
            NotFound();

        return Ok(user);
    }


}
