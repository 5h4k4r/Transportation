using System.Text.Json;
using BabyCareApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.PaygridApi.Helpers;
using Transportation.Api.Http;
using Transportation.Api.Model;

namespace Transportation.Api.Controllers;

[Authorize]
[ApiController]
[Route("v1/auth")]
public class AuthController : ControllerBase
{

    private static transportationContext context;
    private readonly ILogger<AuthController> _logger;

    public record AuthRequest(string Mobile);

    public AuthController(ILogger<AuthController> logger, transportationContext Context)
    {
        _logger = logger;
        context = Context;
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

    [Authorize]
    [HttpGet("info")]
    public async Task<ActionResult<User>> GetAuthInfo([FromServices] UserAuthContext authContext)
    {
        var authUser = authContext.GetAuthUser();
        var user = await context.Users
                            .Where(x => x.AuthId == authUser.Id)
                            .Include(x => x.Gender)
                            .ThenInclude(x => x.GenderTranslations)
                            .Include(x => x.UserAreas)
                            .FirstOrDefaultAsync();

        // .Where(user => user.RoleUsers.Where(role => (int)role.RoleId == 2).FirstOrDefault() != null).FirstOrDefault();

        // var person = (from users in context.Users
        //               join employee in context.Employees
        //               on users.AuthId equals "610fb39200b1010010be304a"
        //               join s in context.Accounts
        //               on users.AuthId equals "610fb39200b1010010be304a"
        //               join t in context.AreaDepartments
        //               on s.UserId equals 11
        //               where t.Id == 11
        //               select new
        //               {
        //                   ID = e.BusinessEntityID,
        //                   FirstName = p.FirstName,
        //                   MiddleName = p.MiddleName,
        //                   LastName = p.LastName,
        //                   Region = t.CountryRegionCode
        //               }).FirstOrDefault();

        if (user is null)
            return NotFound();

        return Ok(new ApiResponse<User>(user));
    }


}
