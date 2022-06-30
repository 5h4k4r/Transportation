using System.Net.Mime;
using System.Text.Json;
using Api.Extensions;
using AutoMapper;
using Core.Helpers;
using Core.Models.Base;
using Core.Models.Common;
using Core.Models.Exceptions;
using Core.Models.Requests;
using Core.Models.Responses;
using Infra.Entities;
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
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ServicesController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(List<ListServicesResponses>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListServices()
    {
        var services = await _unitOfWork.Services.ListServices(User.GetLanguageId());
        if (services.Count == 0)
            return NotFound(new BasicResponse("No services found"));


        return Ok(services);
    }


    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServiceAreaTypeDtoResponse), StatusCodes.Status200OK)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetServiceById(uint id)

    {
        var service = await _unitOfWork.Services.GetServiceAreaTypeById(id);
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


        var category = await _unitOfWork.Categories.GetCategoryById(request.CategoryId);
        if (category == null)
            throw new NotFoundException("CategoryId is invalid");


        var baseTypes = await _unitOfWork.BaseTypes.GetBaseTypeById(request.BaseTypeId);
        if (baseTypes == null)
            throw new NotFoundException("BaseTypeId is invalid");

        //check UsageId
        var usage = await _unitOfWork.Usages.GetUsageById(request.UsageId);
        if (usage == null)
            throw new NotFoundException("UsageId is invalid");

        var serviceAreaTypeParams = new ServiceAreaTypeParams
        {
            BasePrice = request.BasePrice,
            BaseTime = request.BaseTime,
            BaseStop = request.BaseStop,
            BaseStopTime = request.BaseStopDistance,
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
        var paramsString = ServiceHelper.PrepareResponse(serviceAreaTypeParams);

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

    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CreateServiceAreaTypeResponse), StatusCodes.Status200OK)]
    [HttpPut("activate/{serviceId}")]
    public async Task<IActionResult> UpdateServiceAreaType(uint serviceId, UpdateServiceAreaTypeRequest request)
    {
        var databaseModel = await _unitOfWork.Services.GetServiceAreaTypeById(serviceId);
        if (databaseModel == null)
            return NotFound(new BasicResponse("No service found"));

        //if(request.Icon.HasValue)
        //TODO: add service Icon when created FileRepository

        var serviceParams = ServiceHelper.PrepareParams(databaseModel.Params);
        if (serviceParams != null)
        {
            serviceParams.BasePrice = request.BasePrice ?? serviceParams.BasePrice;
            serviceParams.BaseDistance = request.BaseDistance ?? serviceParams.BaseDistance;
            serviceParams.BaseTime = request.BaseTime ?? serviceParams.BaseTime;
            serviceParams.BaseStop = request.BaseStop ?? serviceParams.BaseStop;
            serviceParams.BaseStopTime = request.BaseStopDistance ?? serviceParams.BaseStopTime;
            serviceParams.MinPrice = request.MinPrice ?? serviceParams.MinPrice;
            serviceParams.BaseNight = request.BaseNight ?? serviceParams.BaseNight;
            serviceParams.BaseNightPeriods = new List<BaseNightPeriods>
            {
                new()
                {
                    BaseNightStart = request.BaseNightStart ?? serviceParams.BaseNightPeriods.First().BaseNightStart,
                    BaseNightEnd = request.BaseNightEnd ?? serviceParams.BaseNightPeriods.First().BaseNightEnd
                }
            };
            serviceParams.Tip = request.Tip ?? serviceParams.Tip;
            serviceParams.MinTip = request.MinTip ?? serviceParams.MinTip;
            serviceParams.MaxTip = request.MaxTip ?? serviceParams.MaxTip;
        }

        var paramsString = ServiceHelper.PrepareResponse(serviceParams!);
        databaseModel.Params = paramsString;

        databaseModel.UpdatedAt = DateTime.UtcNow;
        var updatedService = await _unitOfWork.Services.UpdateServiceAreaType(databaseModel);
        await _unitOfWork.Save();

        var response = _mapper.Map<ServiceAreaTypeDto>(updatedService);
        return Ok(response);
    }

    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CommissionDto), StatusCodes.Status200OK)]
    [HttpPut("activate/commission")]
    public async Task<IActionResult> AddCommission(CreateCommissionRequest request)
    {
        //prepare variables
        var newCommission = new Commission
        {
            ServiceAreaTypeId = request.ServiceAreaTypeId,
            Value = request.Value / 100.0,
            IsWithdrawFromGift = request.IsWithdrawFromGift,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        var addedCommission = null as Commission;
        var updatedCommission = null as Commission;

        //get serviceAreaType
        var serviceAreaType = await _unitOfWork.Services.GetServiceAreaTypeById(request.ServiceAreaTypeId);
        if (serviceAreaType == null)
            return NotFound(new BasicResponse("No service found"));

        //get commission of this serviceAreaType
        var commissions = serviceAreaType.Commissions
            .Where(c => c.DeletedAt == null).ToList();

        //if commission is not exist, create new commission
        if (commissions.Any())
        {
            var commission = await _unitOfWork.Commissions.GetCommissionById(commissions.Last().Id);

            commission!.Value = request.Value / 100.0;
            commission.IsWithdrawFromGift = request.IsWithdrawFromGift;
            commission.UpdatedAt = DateTime.UtcNow;

            updatedCommission = _unitOfWork.Commissions.UpdateCommission(commission);
        }
        //else create new commission
        else
        {
            addedCommission = await _unitOfWork.Commissions.CreateCommission(newCommission);
        }

        await _unitOfWork.Save();

        // map created or updated commission to response
        var response = _mapper.Map<CommissionDto>(updatedCommission ?? addedCommission);

        return Ok(response);
    }

    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(DiscountDto), StatusCodes.Status200OK)]
    [HttpPut("activate/discounts")]
    public async Task<IActionResult> UpdateDiscount(CreateDiscountRequest request)
    {
        //prepare variables
        var periods = new List<List<string>> { new() { request.StartAt.ToString(), request.EndAt.ToString() } };
        var periodsStr = JsonSerializer.Serialize(periods);
        var newDiscount = new Discount
        {
            ServiceAreaTypeId = request.ServiceAreaTypeId,
            Value = request.Value / 100.0,
            Periods = periodsStr,
            Limit = request.Limit ?? null,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        var addedDiscount = null as Discount;
        var updatedDiscount = null as Discount;

        // get serviceAreaType
        var serviceAreaType = await _unitOfWork.Services.GetServiceAreaTypeById(request.ServiceAreaTypeId);
        if (serviceAreaType == null)
            return NotFound(new BasicResponse("No service found"));

        // get discounts of this serviceAreaType
        var discounts = serviceAreaType.Discounts.Where(x => x.DeletedAt == null).ToList();

        // if there are any discounts, update them
        if (discounts.Any())
        {
            var discount = await _unitOfWork.Discounts.GetDiscountById(discounts.Last().Id);

            discount!.Value = request.Value / 100.0;
            discount.Max = request.Max;
            discount.Limit = request.Limit ?? null;
            discount.Periods = periodsStr;
            discount.UpdatedAt = DateTime.UtcNow;

            updatedDiscount = _unitOfWork.Discounts.UpdateDiscount(discount);
        }
        // else create new one
        else
        {
            addedDiscount = await _unitOfWork.Discounts.CreateDiscount(newDiscount);
        }

        await _unitOfWork.Save();

        // map created or updated discount to response
        var response = _mapper.Map<DiscountDto>(updatedDiscount ?? addedDiscount);

        return Ok(response);
    }
}

// "icon": "",
// "id": "1",
// "base_price": "5000",
// "base_distance": "0.9",
// "base_time": "2.5",
// "base_stop": "150",
// "base_stop_time": "2.6",
// "min_price": "5000",
// "tip": "1000",
// "min_tip": 0,
// "max_tip": 10,
// "base_night": "1.3",
// "base_night_start": "00:22:45",
// "base_night_end": "06:22:45"