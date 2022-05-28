using System.Net.Mime;
using Core.Interfaces;
using Core.Models.Base;
using Core.Models.Common;
using Core.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.V3;

[Authorize]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[Route("v3/languages")]
public class LanguagesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public LanguagesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    [ProducesResponseType(typeof(List<LanguageDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [HttpGet]
    public async Task<ActionResult> ListLanguages([FromQuery] ListLanguagesRequest model)
    {
        var isLocalRequest = model.LocaleOnly.HasValue && model.LocaleOnly.Value;


        if (isLocalRequest)
        {
            var locales = await _unitOfWork.Languages.ListLanguagesLocales();

            return Ok(locales);
        }

        var languages = await _unitOfWork.Languages.ListLanguages();

        return Ok(languages);
    }


    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult> CreateLanguage([FromBody] CreateLanguageRequest language)
    {
        _unitOfWork.Languages.CreateLanguage(language);

        await _unitOfWork.Save();


        return Ok(BasicResponse.Successful);
    }
}