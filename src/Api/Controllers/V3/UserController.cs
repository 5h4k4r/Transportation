using System.Net.Mime;
using Api.Helpers;
using Core.Interfaces;
using Core.Models.Base;
using Core.Models.Common;
using Core.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.V3;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[ProducesResponseType(typeof(BasicResponse), StatusCodes.Status401Unauthorized)]
[Route("v3/users")]
[Authorize]
public class UserController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public UserController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [ProducesResponseType(typeof(List<IsUserExistResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [HttpGet("{phone}/exists")]
    public async Task<IActionResult> IsUserExist(string phone)
    {
        var response = new IsUserExistResponse();
        response.IsUser = false;
        response.User = null;
        response.IsServant = false;

        var user = await _unitOfWork.User.GetUserByPhone(UserHelper.PreparePhoneNumber(phone));
        var servant = null as ServantDto;
        if (user is not null && user.AreaId.HasValue)
        {
            servant = await _unitOfWork.Servants.GetServantByUserId((int)user.Id, (ulong)user.AreaId);
            response.IsUser = true;
        }
        else if (user is { AreaId: null })
        {
            servant = await _unitOfWork.Servants.GetServantByUserId((int)user.Id);
            response.IsUser = true;
        }

        if (servant is not null)
            response.IsServant = true;

        response.IsUser = true;
        response.User = user;

        return Ok(response);
    }
}