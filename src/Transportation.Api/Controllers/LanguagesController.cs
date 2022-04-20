namespace Tranportation.Api.Controllers;

using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Tranportation.Api.Requests;
using Tranportation.Api.Responses;
using Transportation.Api.Interfaces;
using Transportation.Api.Models.Common;

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

    [ProducesResponseType(typeof(List<LanguageResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [HttpGet]
    public async Task<ActionResult> ListLanguages()
    {
        var languages = await _unitOfWork.Languages.ListLanguages();

        if (languages is null)
            return NotFound(BasicResponse.ResourceNotFound);

        List<LanguageResponse> response = languages.Select(x => new LanguageResponse
        {
            Id = x.Id,
            Title = x.Title,
            Locale = x.Locale,
            Direction = x.Direction,
            CreatedAt = x.CreatedAt,
            UpdatedAt = x.UpdatedAt
        }).ToList();


        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult> CreateLanguage([FromBody] CreateLanguageRequest Language)
    {
        try
        {
            var language = (await _unitOfWork.Languages.CreateLanguage(Language)).Entity;

            _unitOfWork.Save();


            return Ok(new LanguageResponse
            {
                Id = language.Id,
                Title = language.Title,
                Locale = language.Locale,
                Direction = language.Direction,
                CreatedAt = language.CreatedAt,
                UpdatedAt = language.UpdatedAt
            });
        }
        catch (DbUpdateException ex)
        {
            var sqlException = _unitOfWork.GetException<MySqlException>(ex);

            if (sqlException != null
                && (sqlException.Number == 2627 || sqlException.Number == 2601 || sqlException.Number == 1062))
                return BadRequest(BasicResponse.DuplicateEntry(Language.Locale));


            return BadRequest();
        }
    }
}