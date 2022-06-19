using System.Net.Mime;
using Api.Helpers;
using Core.Models.Base;
using Core.Models.Common;
using Core.Models.Exceptions;
using Core.Models.Responses;
using Infra.Interfaces;
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
        var response = new IsUserExistResponse
        {
            IsUser = false,
            User = null,
            IsServant = false
        };

        var user = await _unitOfWork.User.GetUserByPhone(UserHelper.PreparePhoneNumber(phone));
        if (user is null)
            throw new NotFoundException();

        ServantDto? servant = null;

        servant = await _unitOfWork.Servants.GetServantByUserId(user.Id, user.AreaId ?? 0);

        if (servant is not null)
        {
            response.IsUser = true;
            response.IsServant = true;
        }
        else
        {
            response.IsUser = true;
        }

        response.User = user;

        return Ok(response);
    }
}