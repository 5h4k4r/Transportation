using System.Net.Mime;
using Api.Helpers;
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

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[ProducesResponseType(typeof(BasicResponse), StatusCodes.Status401Unauthorized)]
[Route("v3/vehicles")]
[Authorize]
public class VehiclesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public VehiclesController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [ProducesResponseType(typeof(PaginatedResponse<VehicleDtoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [HttpGet]
    public async Task<IActionResult> ListVehicles([FromQuery] ListVehiclesRequest model)
    {
        var vehicle = await _unitOfWork.Vehicles.ListVehicle(model);
        var vehicelsCount = await _unitOfWork.Vehicles.ListVehicleCount(model);
        return Ok(new PaginatedResponse<VehicleDtoResponse>(vehicelsCount, model, vehicle));
    }

    [ProducesResponseType(typeof(VehicleDtoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetVehicle(ulong id)
    {
        var vehicle = await _unitOfWork.Vehicles.GetDetailedVehicleById(id);
        if (vehicle is null)
            return NotFound(BasicResponse.ResourceNotFound);

        return Ok(vehicle);
    }

    [ProducesResponseType(typeof(List<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [HttpGet("crews/{id}")]
    public async Task<IActionResult> GetVehicleCrew(ulong id, [FromQuery] GetVehicleCrewRequest request)
    {
        List<UserDto> crew;
        if (request.VehicleCrew.Equals(VehicleCrew.Owner))
            crew = await _unitOfWork.Vehicles.GetVehicleOwners(id);
        else
            crew = await _unitOfWork.Vehicles.GetVehicleUsers(id);


        if (crew.Count == 0)
            return NotFound(BasicResponse.ResourceNotFound);


        return Ok(crew);
    }

    [ProducesResponseType(typeof(VehicleDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<IActionResult> CreateVehicle([FromBody] CreateVehicleRequest request)
    {
        var newVehicle = _mapper.Map<VehicleDto>(request);


        var plaqueString = VehicleHelper.PlaqueToString(request.Plaque);
        var newVehicleDetail = _mapper.Map<VehicleDetailDto>(request);
        newVehicleDetail.Plaque = plaqueString!;

        newVehicleDetail.Vehicle = newVehicle;

        _unitOfWork.BeginTransaction();
        // Create vehicle and vehicle detail
        var addedVehicle = await _unitOfWork.Vehicles.AddVehicleDetail(newVehicleDetail);
        await _unitOfWork.Save();

        //if servantId is presented add (user,owner) to Vehicle 
        if (request.ServantId is { } or > 0)
            await _unitOfWork.Vehicles.AddServantToVehicle(addedVehicle.Id, (ulong)request.ServantId);

        //subscribe servant and vehicle to service
        if (request.ServiceAreaTypes != null && request.ServiceAreaTypes.Count > 0)
            await _unitOfWork.Vehicles.SubscribeToService(addedVehicle.VehicleId, request.ServiceAreaTypes,
                request.ServantId);

        await _unitOfWork.Save();

        //prepare documents model for vehicle
        var documentToPrepare = new List<string> { "CarCard", "CarCardBack", "TechDiagnosis", "Insurance" };
        var documents = PrepareDocuments(request, documentToPrepare);

        //Add vehicles documents
        _unitOfWork.Document.AddDocuments(documents, "App\\Models\\Vehicle", addedVehicle.Id);
        await _unitOfWork.Save();

        _unitOfWork.EndTransaction();
        
        return Ok(addedVehicle);
    }

    [ProducesResponseType(typeof(VehicleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVehicle(ulong id, [FromBody] UpdateVehicleRequest request)
    {
        var vehicle = await _unitOfWork.Vehicles.GetVehicleById(id);
        if (vehicle is null)
            throw new NotFoundException();
        //setting new vehicle values
        vehicle.Title = request.Title ?? vehicle.Title;
        vehicle.UsageId = request.UsageId ?? vehicle.UsageId;
        vehicle.UpdatedAt = DateTime.UtcNow;

        // setting new vehicleDetail values if exists
        if (request.VehicleDetails != null && request.VehicleDetails.Any())
        {
            var reqVehicleDetail = request.VehicleDetails.First();
            var plaqueDtoResponse = reqVehicleDetail.Plaque;
            string? plaqueString = null;
            if (plaqueDtoResponse != null) plaqueString = VehicleHelper.PlaqueToString(plaqueDtoResponse);

            var vehicleDetail = vehicle.VehicleDetails.First();

            vehicleDetail.UpdatedAt = DateTime.UtcNow;
            vehicleDetail.Plaque = plaqueString ?? vehicleDetail.Plaque;
            vehicleDetail.Color = reqVehicleDetail.Color ?? vehicleDetail.Color;
            vehicleDetail.Model = reqVehicleDetail.Model ?? vehicleDetail.Model;
            vehicleDetail.Tip = reqVehicleDetail.Tip ?? vehicleDetail.Tip;
            vehicleDetail.Vin = reqVehicleDetail.Vin ?? vehicleDetail.Vin;
            vehicleDetail.InsuranceNo = reqVehicleDetail.InsuranceNo ?? vehicleDetail.InsuranceNo;
            vehicleDetail.InsuranceExpire = reqVehicleDetail.InsuranceExpire ?? vehicleDetail.InsuranceExpire;
        }

        var mappedRequestToDocuments = _mapper.Map<CreateVehicleRequest>(request);

        var updatedVehicle = await _unitOfWork.Vehicles.UpdateVehicle(vehicle);
        await _unitOfWork.Save();

        var documentToPrepare = new List<string> { "CarCard", "CarCardBack", "TechDiagnosis", "Insurance" };
        var documents = PrepareDocuments(mappedRequestToDocuments, documentToPrepare);

        //TODO: update documents in document repository

        var response = _mapper.Map<VehicleDto>(updatedVehicle);

        return Ok(response);
    }

    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status400BadRequest)]
    [HttpPost("servant")]
    public async Task<IActionResult> AddSeravntToVehicle([FromBody] AddServantToVehicleRequest request)
    {
        await _unitOfWork.Vehicles.AddServantToVehicle(request.VehicleId, request.UserId);
        await _unitOfWork.Save();

        return Ok(BasicResponse.Successful);
    }

    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status400BadRequest)]
    [HttpPost("owner")]
    public async Task<IActionResult> AddOwnerToVehicle([FromBody] AddServantToVehicleRequest request)
    {
        await _unitOfWork.Vehicles.AddOwnerToVehicle(request.VehicleId, request.UserId);
        await _unitOfWork.Save();

        return Ok(BasicResponse.Successful);
    }

    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status400BadRequest)]
    [HttpPost("user")]
    public async Task<IActionResult> AddUserToVehicle([FromBody] AddServantToVehicleRequest request)
    {
        await _unitOfWork.Vehicles.AddUserToVehicle(request.VehicleId, request.UserId);
        await _unitOfWork.Save();

        return Ok(BasicResponse.Successful);
    }


    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVehicle(ulong id)
    {
        await _unitOfWork.Vehicles.DeleteVehicle(id);
        await _unitOfWork.Save();

        return Ok(BasicResponse.Successful);
    }

    private List<Document> PrepareDocuments(CreateVehicleRequest request, List<string> documentsToPrepare)
    {
        var documents = new List<Document>();
        var namingPolicy = new SnakeCaseNamingPolicy();
        for (var index = 0; index < request.GetType().GetProperties().Length; index++)
        {
            var p = request.GetType().GetProperties()[index];
            foreach (var doc in documentsToPrepare)
                if (p.Name == doc)
                    documents.Add(new Document
                    {
                        Type = namingPolicy.ConvertName(p.Name),
                        Path = p.GetValue(request, null)?.ToString()
                    });
            // if (p.Name is "CarCard" or "CarCardBack" or "TechDiagnosis" or "Insurance")
            //     documents.Add(new Document
            //     {
            //         Type = namingPolicy.ConvertName(p.Name),
            //         Path = p.GetValue(request, null)?.ToString()
            //     });
        }

        return documents;
    }
}