using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tranportation.Api.Requests;
using Transportation.Api.Common;
using Transportation.Api.Interfaces;
using Transportation.Api.Models.Common;
using Transportation.Api.Repositories;
using Transportation.Api.Requests;
using Transportation.Api.Responses;

namespace Transportation.Api.Controllers;

[Authorize]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[Route("v3/servants")]
public class ServantsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ServantsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    /// <summary>
    /// Gets a servant's performance
    /// </summary>
    [HttpGet("{id}/performance")]
    [ProducesResponseType(typeof(ServantPerformanceWithUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]

    public async Task<ActionResult> ServantPerformance(int Id, [FromQuery] ServantPerformanceRequest model)
    {
        model.UserId = (ulong)Id;
        // The servant we get from database
        Model.Servant? databaseServant = await _unitOfWork.Servants.GetServantById(model.UserId.GetValueOrDefault());

        if (databaseServant == null)
            return NotFound(BasicResponse.ResourceDoesNotExist(nameof(ServantPerformed), (int)model.UserId!));

        // The servant we send back as a response
        ServantPerformed? responseServant = new()
        {
            Id = databaseServant.Id,
            UserId = databaseServant.UserId,
            FirstName = databaseServant.FirstName,
            LastName = databaseServant.LastName,
            NationalId = databaseServant.NationalId,
            Certificate = databaseServant.Certificate,
            BankId = databaseServant.BankId,
            AreaId = databaseServant.AreaId,
            Address = databaseServant.Address,
            Rating = databaseServant.ServantScores.Select(x => x.Score).FirstOrDefault()
        };

        ServantPerformance? servantPerformance = await _unitOfWork.Servants.GetServantPerformance(model, responseServant.Id);

        if (servantPerformance == null)
            return NotFound();

        ServantPerformanceWithUserResponse response = new()
        {
            Performance = servantPerformance,
            Servant = responseServant
        };

        return Ok(response);

    }


    [HttpGet("{Id}/online")]
    public async Task<IActionResult> GetServantOnlinePeriods(int Id, [FromQuery] ServantOnlinePeriodRequest model)
    {


        var servant = await _unitOfWork.Servants.GetServantById((ulong)Id);

        if (servant is null)
            return NotFound();


        var response = await _unitOfWork.ServantWorkDays.GetServantWorkDays((ulong)Id, model);



        if (response is null)
            return NotFound();


        return Ok(response);
    }


    [HttpGet("online-history")]
    public async Task<IActionResult> ListServantsOnlineHistory([FromQuery] ServantOnlinePeriodRequest model)
    {


        var response = await _unitOfWork.ServantWorkDays.ListServantsOnlineHistory(model);

        if (response is null)
            return NotFound();



        return Ok(response);
    }

}