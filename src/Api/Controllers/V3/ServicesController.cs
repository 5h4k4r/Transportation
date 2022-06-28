using System.Net.Mime;
using Core.Helpers;
using Core.Models.Base;
using Core.Models.Common;
using Core.Models.Exceptions;
using Core.Models.Requests;
using Core.Models.Responses;
using Infra.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.V3;

[Authorize]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[Route("v3/services")]
public class ServicesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ServicesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(List<ListServicesResponses>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListServices()
    {
        var services = await _unitOfWork.Services.ListServices();
        if (services.Count == 0)
            return NotFound(new BasicResponse("No services found"));


        return Ok(services);
    }


    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServiceAreaTypeDtoResponse), StatusCodes.Status200OK)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetServiceById(uint id, uint serviceId)

    {
        var service = await _unitOfWork.Services.GetServiceById(id, serviceId);
        if (service == null)
            return NotFound(new BasicResponse("No service found"));

        return Ok(service);
    }

    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CreateServiceAreaTypeResponse), StatusCodes.Status200OK)]
    [HttpPost("activate/{serviceId}")]
    public async Task<IActionResult> CreateServiceAreaType(uint serviceId, CreateServiceAreaTypeRequest request)

    {
        //TODO: add service Icon when created FileRepository

        // check AreaId
        var area = await _unitOfWork.AreaInfos.GetAreaInfoByAreaId(request.AreaId);
        if (area == null)
            throw new NotFoundException("AreaId is invalid");

        //TODO: check CategoryId

        //TODO: check BaseTypeId

        //check UsageId
        var usage = await _unitOfWork.Usages.GetUsageById(request.UsageId);
        if (usage == null)
            throw new NotFoundException("UsageId is invalid");

        var servieAreaTypeParams = new ServiceAreaTypeParams
        {
            BasePrice = request.BasePrice,
            BaseTime = request.BaseTime,
            BaseStop = request.BaseStop,
            BaseStopDistance = request.BaseStopDistance,
            BaseDistance = request.BaseDistance,
            MinPrice = request.MinPrice,
            BaseNight = request.BaseNight,
            BaseNightPeriods = new List<BaseNightPeriods>
            {
                new()
                {
                    BaseNightStart = request.BaseNightStart,
                    BaseNightEnd = request.BaseNightEnd
                }
            },
            Tip = request.Tip,
            MinTip = request.MinTip,
            MaxTip = request.MaxTip
        };
        var paramsString = ServiceHelper.PrepareResponse(servieAreaTypeParams);

        var serviceAreaType = new ServiceAreaTypeDto
        {
            ServiceId = serviceId,
            AreaId = request.AreaId,
            CategoryId = request.CategoryId,
            TypeId = request.BaseTypeId,
            UsageId = request.UsageId,
            Params = paramsString,
            Currency = area.Currency,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            DeletedAt = null
        };

        var service = await _unitOfWork.Services.CreateServiceAreaType(serviceAreaType);
        await _unitOfWork.Save();

        var response = new CreateServiceAreaTypeResponse
        {
            ServiceAreaTypeId = service.Id,
            ServiceId = serviceId
        };

        return Ok(response);
    }
}