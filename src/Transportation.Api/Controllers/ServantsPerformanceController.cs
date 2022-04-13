using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
[Route("v3/servants/performance")]
public class ServantsPerformanceController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ServantsPerformanceController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    /// <summary>
    /// Gets a servant's performance
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ServantPerformanceWithUserResponse?>> ServantPerformance([FromQuery] ServantPerformanceRequest model)
    {

        // The servant we get from database
        Model.Servant? databaseServant = await _unitOfWork.ServantPerformance.GetServantById((int)model.UserId);

        if (databaseServant == null)
            return NotFound(BasicResponse.ResourceDoesNotExist(nameof(Servant), (int)model.UserId));

        // The servant we send back as a response
        Servant? responseServant = new()
        {
            UserId = databaseServant.UserId,
            FirstName = databaseServant.FirstName,
            LastName = databaseServant.LastName,
            NationalId = databaseServant.NationalId,
            Certificate = databaseServant.Certificate,
            BankId = databaseServant.BankId,
            AreaId = databaseServant.AreaId,
            Address = databaseServant.Address,
        };

        ServantPerformance? servantPerformance = await _unitOfWork.ServantPerformance.GetServantPerformance(model);

        if (servantPerformance == null)
            return NotFound();

        ServantPerformanceWithUserResponse response = new()
        {
            Performance = servantPerformance,
            Servant = responseServant
        };

        return response;

    }
}