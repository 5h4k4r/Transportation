using System.Net.Mime;
using Api.Extensions;
using AutoMapper;
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
    ///     Get Servant by Id
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

        var totalOnlineTime = await _unitOfWork.ServantWorkDays.GetServantOnlinePeriodsTotalTime(model, (ulong)id);
        var totalTimes = new ServantOnlinePeriodTotalTime
        {
            TotalOnlineTime = totalOnlineTime.OnlineHours,
            TotalOnlineTimeInSeconds = totalOnlineTime.TotalTimeInSeconds
        };


        return Ok(new PaginatedResponse<ServantOnlinePeriod>(servantOnlineHistoryCount, model, servantOnlineHistory,
            totalTimes));
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
        await _unitOfWork.Servants.CreateServant(servant);
        await _unitOfWork.User.GetUserById(request.UserId);

        // await _unitOfWork.Save();


        //prepare document for servant
        var documentsToPrepare = new List<string>
            { "certificate", "certificate_back", "national_card_back", "avatar", "national_card" };

        if (request.Documents != null)
        {
            var documents = PrepareDocuments(request.Documents, documentsToPrepare);
            if (documents.Count < 5)
                throw new BadRequestException(
                    "Documents must include Certificate, CertificateBack, NationalCardBack, Avatar, NationalCard");
            await _unitOfWork.Document.AddDocuments(documents, "App\\Models\\Servant", request.UserId);
        }

        if (!User.HasRole(Role.Servant))
            await _unitOfWork.RoleUsers.AddRoleUser(new RoleUserDto
            {
                RoleId = (byte)Role.Servant,
                UserId = request.UserId
            });


        var model = new AddServantToWalletServiceRequest
        {
            group = "driver",
            userId = "608560b2a9ac9b001092bd97",
            IBan = ""
        };

        var wallet = _serviceProvider.GetRequiredService<IOptions<WalletOptions>>().Value;

        // TODO: Fix integrating the api with wallet
        await _curl.Send($"{wallet.ServerUrl}/service/user-groups/store", true, true, model, true, true);


        await _unitOfWork.Save();
        _unitOfWork.EndTransaction();


        return Ok(BasicResponse.Successful);
    }

    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status400BadRequest)]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateServant([FromBody] UpdateServantRequest request, ulong id)
    {
        if (!User.GetAreaId().HasValue && !User.HasRole(Role.SuperAdmin))
            throw new UnauthorizedException();


        if (request.AreaId.HasValue && await _unitOfWork.AreaInfos.GetAreaInfoById(request.AreaId.Value) is null)
            throw new NotFoundException("The Area you are trying to assign the servant to does not exist");

        _unitOfWork.BeginTransaction();

        await _unitOfWork.Servants.UpdateServant(request, id);

        // await _unitOfWork.Save();

        var documentsToPrepare = new List<string>
            { "certificate", "certificate_back", "national_card_back", "avatar", "national_card" };

        if (request.Documents != null)
        {
            var documents = PrepareDocuments(request.Documents, documentsToPrepare);

            _unitOfWork.Document.UpdateDocuments(documents);
        }


        await _unitOfWork.Save();

        _unitOfWork.EndTransaction();


        return Ok(request);
    }

    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteServant(ulong id)
    {
        if (!User.GetAreaId().HasValue && !User.HasRole(Role.SuperAdmin))
            throw new UnauthorizedException();


        var servant = await _unitOfWork.Servants.GetServantByUserId(id, User.GetAreaId()!.Value);

        if (servant is null)
            throw new NotFoundException($"No servant found with userId {id}");

        if (await _unitOfWork.Servants.ServantActiveTask(id) is not null)
            throw new BadRequestException("Servant still has an active task");

        await _unitOfWork.Servants.DeleteServant(servant);
        await _unitOfWork.Save();
        return Ok(BasicResponse.Successful);
    }

    [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [HttpGet("{id}/services")]
    public async Task<IActionResult> ListServiceSubscribers(ulong id)
    {
        var langId = User.GetLanguageId() ?? 2;
        var services = await _unitOfWork.Servants.GetServantsServices(id, langId);

        return Ok(services);
    }

    [ProducesResponseType(typeof(ListServantsWithTheirStatusesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status401Unauthorized)]
    [HttpGet("servant-statuses")]
    public async Task<IActionResult> ListServantsWithTheirStatuses(
        [FromQuery] ListServantsWithTheirStatusesRequest model)
    {
        if ((!User.GetAreaId().HasValue && !User.HasRole(Role.SuperAdmin)) || !User.HasRole(Role.Employee))
            throw new UnauthorizedException();

        await _unitOfWork.Employees.GetEmployeeByUserId(User.UserId()!.Value);

        var servants = await _unitOfWork.Servants.ListServantsWithTheirStatuses(User.GetAreaId()!.Value, model);

        if (servants is null)
            throw new NotFoundException($"No servants found with this status : {model.Status}");

        return Ok(servants);
    }

    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status400BadRequest)]
    [HttpPost("{id}/change-status")]
    public async Task<IActionResult> ChangeServantStatus(ulong id, ChangeServantStatusRequest model)
    {
        if ((!User.GetAreaId().HasValue && !User.HasRole(Role.SuperAdmin)) || !User.HasRole(Role.Employee))
            throw new UnauthorizedException();

        var servant = await _unitOfWork.Servants.GetServantByUserId(id, User.GetAreaId()!.Value);

        if (servant is null)
            throw new NotFoundException($"No servant found with id: {id}");

        var servantStatus = await _unitOfWork.ServantStatus.GetServantStatus(id);

        if (servantStatus is null)
            throw new NotFoundException($"No Servant Status found for servant id: {id}");
        
        
        _unitOfWork.ServantStatus.ChangeServantStatus(servantStatus, model.Status);
        
        await _unitOfWork.Save();
        
        return Ok();
    }


    private List<DocumentDto> PrepareDocuments(List<KeyValuePair<string, string>> docs, List<string> documentsToPrepare)
    {
        var documents = new List<DocumentDto>();
        for (var index = 0; index < docs.Count; index++)
            if (documentsToPrepare?.Count(x => string.Equals(x, docs[index].Key)) > 0)
                documents.Add(
                    new DocumentDto
                    {
                        Type = docs[index].Key,
                        Path = docs[index].Value
                    }
                );

        return documents;
    }
}