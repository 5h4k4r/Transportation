namespace Tranportation.Api.Controllers;

using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Tranportation.Api.Requests;
using Tranportation.Api.Responses;
using Transportation.Api.Auth;
using Transportation.Api.Interfaces;
using Transportation.Api.Models.Common;

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


    [ProducesResponseType(typeof(List<Language>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [HttpGet]
    public async Task<ActionResult> ListLanguages([FromQuery] ListLanguagesRequest model)
    {

        var isLocalRequest = model.LocaleOnly.HasValue && model.LocaleOnly.Value;


        if (isLocalRequest)
        {
            var locales = await _unitOfWork.Languages.ListLanguagesLocales();

            if (locales is null)
                return NotFound();

            return Ok(locales);
        }
        var languages = (await _unitOfWork.Languages.ListLanguages()).Select(x => new LanguageResponse(x));



        if (languages is null)
            return NotFound(BasicResponse.ResourceNotFound);


        return Ok(languages);
    }


    [ProducesResponseType(typeof(LanguageResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult> CreateLanguage([FromBody] CreateLanguageRequest Language)
    {
        try
        {
            var language = (await _unitOfWork.Languages.CreateLanguage(Language)).Entity;


            _unitOfWork.Save();

            return Ok(new LanguageResponse(language));
        }
        catch (DbUpdateException ex)
        {
            var sqlException = _unitOfWork.GetException<MySqlException>(ex);

            if (sqlException != null
                && (sqlException.Number == 1062))
                return BadRequest(BasicResponse.DuplicateEntry(Language.Locale));


            return BadRequest();
        }
    }
}