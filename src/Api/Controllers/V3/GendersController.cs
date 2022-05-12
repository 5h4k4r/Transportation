using System.Net.Mime;
using AutoMapper;
using Core.Helpers;
using Core.Interfaces;
using Core.Models;
using Infra.Entities.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[Route("v3/genders")]
public class GendersController : ControllerBase
{

    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public GendersController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [ProducesResponseType(typeof(List<GenderTranslationDTO>), StatusCodes.Status200OK)]
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

        var Gender = await _unitOfWork.Genders.ListGenders(user.LanguageId.Value);

        if (Gender.Count == 0)
            return NotFound(BasicResponse.ResourceDoesNotExist(nameof(Gender), (int)user.LanguageId, nameof(user.LanguageId)));

        return Ok(Gender);
    }



}