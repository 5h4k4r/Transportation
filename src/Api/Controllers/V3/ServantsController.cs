using System.Net.Mime;
using AutoMapper;
using Core.Interfaces;
using Core.Models.Base;
using Core.Models.Common;
using Core.Models.Repositories;
using Core.Models.Requests;
using Infra.Authentication;
using Infra.Entities;
using Infra.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.V3;

[Authorize]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[Route("v3/servants")]
public class ServantsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ServantsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    /// <summary>
    /// List Servants
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(PaginatedResponse<ServantDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListServants(int id, [FromQuery] ListServantRequest model, [FromServices] UserAuthContext authContext)
    {
        var authId = authContext.GetAuthUser().Id;
        var user = await _unitOfWork.User.GetUserByAuthId(authId);

        if (user is null || !user.AreaId.HasValue)
            return Unauthorized(BasicResponse.AuthenticationFailed);


        // The servant we get from database
        var items = await _unitOfWork.Servants.ListServants(model, user.AreaId.Value);
        var count = await _unitOfWork.Servants.ListServantsCount(model, user.AreaId.Value);


        return Ok(new PaginatedResponse<ServantDto>(count, model, items));


    }

    /// <summary>
    /// List Servants
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(PaginatedResponse<ServantDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetServantById(int id, [FromServices] UserAuthContext authContext)
    {
        var authId = authContext.GetAuthUser().Id;
        var user = await _unitOfWork.User.GetUserByAuthId(authId);

        if (user is null || !user.AreaId.HasValue)
            return Unauthorized(BasicResponse.AuthenticationFailed);


        // The servant we get from database
        var servant = await _unitOfWork.Servants.GetServantById(id, user.AreaId.Value);

        if (servant is null)
            return NotFound(BasicResponse.ResourceNotFound);

        return Ok(servant);


    }


    /// <summary>
    /// Gets a servant's performance
    /// </summary>
    [HttpGet("{id}/performance")]
    [ProducesResponseType(typeof(ServantPerformanceWithUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]


    public async Task<ActionResult> ServantPerformance(int id, [FromQuery] ServantPerformanceRequest model, [FromServices] UserAuthContext authContext)
    {

        var authId = authContext.GetAuthUser().Id;
        var user = await _unitOfWork.User.GetUserByAuthId(authId);

        if (user is null || !user.AreaId.HasValue)
            return Unauthorized(BasicResponse.AuthenticationFailed);


        // The servant we get from database
        var databaseServant = await _unitOfWork.Servants.GetServantById(id, user.AreaId.Value);

        if (databaseServant == null)
            return NotFound(BasicResponse.ResourceDoesNotExist(nameof(ServantPerformed), id));

        // The servant we send back as a response
        ServantPerformed responseServant = new()
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
            Rating = databaseServant.ServantScores?.Select(x => x.Score).FirstOrDefault()
        };

        var servantPerformance = await _unitOfWork.Servants.GetServantPerformance(model, responseServant.Id, (ulong)id);

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
    [ProducesResponseType(typeof(PaginatedResponse<ListTasks>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetServantOnlinePeriods(int id, [FromQuery] GetServantOnlinePeriodsRequest model, [FromServices] UserAuthContext authContext)
    {
        var authId = authContext.GetAuthUser().Id;
        var user = await _unitOfWork.User.GetUserByAuthId(authId);

        if (user is null || !user.AreaId.HasValue)
            return Unauthorized(BasicResponse.AuthenticationFailed);


        // The servant we get from database
        var servant = await _unitOfWork.Servants.GetServantById(id, user.AreaId.Value);

        if (servant is null)
            return NotFound(BasicResponse.ResourceDoesNotExist(nameof(Servant), id));


        var servantWorkDays = await _unitOfWork.ServantWorkDays.GetServantOnlinePeriods((ulong)id, model);
        var servantWorkDaysCount = await _unitOfWork.ServantWorkDays.GetServantOnlinePeriodsCount((ulong)id, model);


        return Ok(
            new PaginatedResponse<ServantOnlinePeriod>(
                servantWorkDaysCount,
                model,
                servantWorkDays.Items.ToList(),
                new
                {
                    TotalOnlineTimeInSecond = servantWorkDays.TotalTimeInSeconds,
                    TotalOnlineTime = servantWorkDays.TotalTime
                }
        ));
    }


    [HttpGet("online-history")]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(PaginatedResponse<ListServantsOnlineHistory>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListServantsOnlineHistory([FromQuery] GetServantOnlinePeriodsRequest model)
    {


        var items = await _unitOfWork.ServantWorkDays.ListServantsOnlineHistory(model);
        var count = await _unitOfWork.ServantWorkDays.ListServantsOnlineHistoryCount(model);

        if (items is null)
            return NotFound(BasicResponse.ResourceNotFound);



        return Ok(new PaginatedResponse<ListServantsOnlineHistory>(count, model, items));
    }

    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public IActionResult CreateServant([FromBody] CreateServantRequest request)
    {
        var servant = _mapper.Map<ServantDto>(request);
        _unitOfWork.Servants.CreateServant(servant);

        return Ok(BasicResponse.Successful);
    }

}