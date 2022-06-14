using System.Net.Mime;
using Api.Extensions;
using AutoMapper;
using Core.Helpers;
using Core.Models.Base;
using Core.Models.Common;
using Core.Models.Exceptions;
using Core.Models.Repositories;
using Core.Models.Requests;
using Core.Models.Responses;
using Core.Settings;
using Infra.Authentication;
using Infra.Entities;
using Infra.Interfaces;
using Infra.Models;
using Infra.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Role = Core.Models.Authentication.Role;

namespace Api.Controllers.V3;

[Authorize]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[Route("v3/servants")]
public class ServantsController : ControllerBase
{
    private readonly ICurl _curl;
    private readonly IMapper _mapper;
    private readonly IServiceProvider _serviceProvider;
    private readonly IUnitOfWork _unitOfWork;

    public ServantsController(IUnitOfWork unitOfWork, IMapper mapper, ICurl curl, IServiceProvider sp)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _curl = curl;
        _serviceProvider = sp;
    }

    /// <summary>
    ///     List Servants
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(PaginatedResponse<ServantDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListServants([FromQuery] ListServantRequest model,
        [FromServices] UserAuthContext authContext)
    {
        if (!User.GetAreaId().HasValue && !User.HasRole(Role.SuperAdmin))
            throw new UnauthorizedException();

        // The servant we get from database
        var items = await _unitOfWork.Servants.ListServants(model, User.GetAreaId()!.Value);
        var count = await _unitOfWork.Servants.ListServantsCount(model, User.GetAreaId()!.Value);


        return Ok(new PaginatedResponse<ServantDto>(count, model, items));
    }

    /// <summary>
    ///     List Servants
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(PaginatedResponse<ServantDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetServantByUserId(int id, [FromServices] UserAuthContext authContext)
    {
        if (!User.GetAreaId().HasValue && !User.HasRole(Role.SuperAdmin))
            throw new UnauthorizedException();

        // The servant we get from database
        var servant = await _unitOfWork.Servants.GetServantByUserId((ulong)id, User.GetAreaId()!.Value);

        if (servant is null)
            return NotFound(BasicResponse.ResourceNotFound);

        return Ok(servant);
    }


    /// <summary>
    ///     Gets a servant's performance
    /// </summary>
    [HttpGet("{id}/performance")]
    [ProducesResponseType(typeof(ServantPerformanceWithUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> ServantPerformance(int id, [FromQuery] ServantPerformanceRequest model)
    {
        if (!User.GetAreaId().HasValue && !User.HasRole(Role.SuperAdmin))
            throw new UnauthorizedException();

        // The servant we get from database
        var databaseServant = await _unitOfWork.Servants.GetServantByUserId((ulong)id, User.GetAreaId()!.Value);

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


    [HttpGet("{id}/online-periods")]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<ServantOnlinePeriod>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetServantOnlinePeriods(int id, [FromQuery] GetServantOnlineHistoryRequest model)
    {
        if (!User.GetAreaId().HasValue && !User.HasRole(Role.SuperAdmin))
            throw new UnauthorizedException();

        model.AreaId = User.GetAreaId()!.Value;

        // The servant we get from database
        var servant = await _unitOfWork.Servants.GetServantByUserId((ulong)id, model.AreaId.Value);

        if (servant is null)
            return NotFound(BasicResponse.ResourceDoesNotExist(nameof(Servant), id));


        var servantOnlineHistory = await _unitOfWork.ServantWorkDays.GetServantOnlinePeriods((ulong)id, model);
        var servantOnlineHistoryCount =
            await _unitOfWork.ServantWorkDays.GetServantOnlinePeriodsCount((ulong)id, model);


        return Ok(new PaginatedResponse<ServantOnlinePeriod>(servantOnlineHistoryCount, model, servantOnlineHistory));
    }


    [HttpGet("online-history")]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<ListServantsOnlineHistory>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListServantsOnlineHistory([FromQuery] ListServantsOnlineHistoryRequest model)
    {
        if (!User.GetAreaId().HasValue && !User.HasRole(Role.SuperAdmin))
            throw new UnauthorizedException();

        model.AreaId = User.GetAreaId()!.Value;

        var items = await _unitOfWork.ServantWorkDays.ListServantsOnlineHistory(model);

        if (items is null)
            return NotFound(BasicResponse.ResourceNotFound);


        return Ok(items);
    }

    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<IActionResult> CreateServant([FromBody] CreateServantRequest request)
    {
        if (await _unitOfWork.AreaInfos.GetAreaInfoById(request.AreaId) is null)
            throw new NotFoundException("The Area you are trying to assign the servant to does not exist");

        _unitOfWork.BeginTransaction();
        var servant = _mapper.Map<ServantDto>(request);
        var createdServant = await _unitOfWork.Servants.CreateServant(servant);
        var user = await _unitOfWork.User.GetUserById(request.UserId);

        await _unitOfWork.Save();

        var documents = new List<Document>();
        var namingPolicy = new SnakeCaseNamingPolicy();


        for (var index = 0; index < request.GetType().GetProperties().Length; index++)
        {
            var p = request.GetType().GetProperties()[index];
            if (
                p.Name is "Certificate" or "CertificateBack" or "NationalCardBack" or "Avatar" or "NationalCard"
            )
                documents.Add(new Document
                {
                    Type = namingPolicy.ConvertName(p.Name),
                    Path = p.GetValue(request, null)?.ToString()
                });
        }

        _unitOfWork.Document.AddDocuments(documents, "App\\Models\\Servant", request.UserId);

        if (!User.HasRole(Role.Servant))
            await _unitOfWork.RoleUsers.AddRoleUser(new RoleUserDto
            {
                RoleId = (byte)Role.Servant,
                UserId = request.UserId
            });

        await _unitOfWork.Save();

        var model = new AddServantToWalletServiceRequest
        {
            group = "driver",
            userId = user.AuthId,
            IBan = null
        };

        var wallet = _serviceProvider.GetRequiredService<IOptions<WalletOptions>>().Value;

        var response = await _curl.Send($"{wallet.ServerUrl}/service/user-groups/store", true, true, model);
        _unitOfWork.EndTransaction();

        return Ok(BasicResponse.Successful);
    }
}