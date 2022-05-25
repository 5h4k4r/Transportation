using System.Net.Mime;
using Core.Interfaces;
using Core.Models.Base;
using Core.Models.Common;
using Infra.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.V3;

[Authorize]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[Route("v3/genders")]
public class GendersController : ControllerBase
{

    private readonly IUnitOfWork _unitOfWork;
    public GendersController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
     
    }

    [ProducesResponseType(typeof(List<GenderTranslationDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [HttpGet]
    public async Task<ActionResult> ListGenders([FromServices] UserAuthContext authContext)
    {

        var authUser = authContext.GetAuthUser();


        var user = await _unitOfWork.User.GetUserByAuthId(authUser.Id);
        if (user is null)
            return NotFound(BasicResponse.ResourceDoesNotExist(nameof(user)));

        // if User does not have LanguageId set it to English
        if (!user.LanguageId.HasValue)
            user.LanguageId = 3;

        var gender = await _unitOfWork.Genders.ListGenders(user.LanguageId.Value);

        if (gender.Count == 0)
            return NotFound(BasicResponse.ResourceDoesNotExist(nameof(gender), (int)user.LanguageId, nameof(user.LanguageId)));

        return Ok(gender);
    }



}