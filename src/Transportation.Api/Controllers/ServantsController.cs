using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tranportation.Api.Requests;
using Tranportation.Api.Responses;
using Transportation.Api.Common;
using Transportation.Api.Interfaces;
using Transportation.Api.Model;
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
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]


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


    [HttpGet("{id}/online")]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(PaginatedResponse<ListTasksResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetServantOnlinePeriods(int id, [FromQuery] ServantWorkDaysRequest model)
    {

        var servant = await _unitOfWork.Servants.GetServantById((ulong)id);

        if (servant is null)
            return NotFound(BasicResponse.ResourceDoesNotExist(nameof(Servant), (int)id));


        var servantWorkDays = await _unitOfWork.ServantWorkDays.GetServantWorkDays((ulong)id, model);
        var servantWorkDaysCount = await _unitOfWork.ServantWorkDays.GetServantWorkDaysCount((ulong)id, model);

        if (servantWorkDays is null)
            return NotFound(BasicResponse.ResourceNotFound);


        return Ok(new PaginatedResponse<ServantWorkDayResponse>(servantWorkDaysCount, model, servantWorkDays.Items));
    }


    [HttpGet("online-history")]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(PaginatedResponse<ListServantsOnlineHistoryResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListServantsOnlineHistory([FromQuery] ListServantsOnlineHistoryRequest model)
    {


        var items = await _unitOfWork.ServantWorkDays.ListServantsOnlineHistory(model);
        // var count = await _unitOfWork.ServantWorkDays.ListServantsOnlineHistoryCount(model);

        if (items is null)
            return NotFound(BasicResponse.ResourceNotFound);



        return Ok(new PaginatedResponse<ListServantsOnlineHistoryResponse>(items.Count, model, items));
    }

}