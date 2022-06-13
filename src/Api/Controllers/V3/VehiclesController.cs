using System.Net.Mime;
using Api.Helpers;
using AutoMapper;
using Core.Models.Base;
using Core.Models.Common;
using Core.Models.Exceptions;
using Core.Models.Requests;
using Core.Models.Responses;
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

    [ProducesResponseType(typeof(PaginatedResponse<VehicleDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [HttpGet]
    public async Task<IActionResult> ListVehicles([FromQuery] ListVehiclesRequest model)
    {
        var vehicle = await _unitOfWork.Vehicles.ListVehicle(model);
        var vehicelsCount = await _unitOfWork.Vehicles.ListVehicleCount(model);
        return Ok(new PaginatedResponse<VehicleDto>(vehicelsCount, model, vehicle));
    }

    [ProducesResponseType(typeof(VehicleDtoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetVehicle(ulong id)
    {
        var vehicle = await _unitOfWork.Vehicles.GetVehicleById(id);
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

        var addedVehicle = await _unitOfWork.Vehicles.AddVehicleDetail(newVehicleDetail);

        //TODO: Add vehicle documents

        await _unitOfWork.Save();
        if (request.ServiceAreaTypes != null && request.ServiceAreaTypes.Count > 0)
            await _unitOfWork.Vehicles.SubscribeVehicleToService(addedVehicle.VehicleId, request.ServiceAreaTypes);

        await _unitOfWork.Save();
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


        var newVehicle = _mapper.Map<VehicleDto>(request);
        newVehicle.CreatedAt = vehicle.CreatedAt;
        newVehicle.UpdatedAt = DateTime.UtcNow;
        newVehicle.Id = id;
        if (newVehicle.VehicleDetails != null && newVehicle.VehicleDetails.Count > 0)
            if (vehicle.VehicleDetail != null)
            {
                var plaqueString = VehicleHelper.PlaqueToString(request.VehicleDetails.First().Plaque);
                var vehicleDetailDto = vehicle.VehicleDetail;


                var newVehicleDetailDto = newVehicle.VehicleDetails.First();
                newVehicleDetailDto.VehicleId = id;
                newVehicleDetailDto.Id = vehicleDetailDto.Id;
                newVehicleDetailDto.CreatedAt = vehicleDetailDto.CreatedAt;
                newVehicleDetailDto.UpdatedAt = DateTime.UtcNow;
                newVehicleDetailDto.Plaque = plaqueString;

                newVehicle.VehicleDetails = new List<VehicleDetailDto> { newVehicleDetailDto };
            }

        var updatedVehicle = await _unitOfWork.Vehicles.UpdateVehicle(newVehicle);


        await _unitOfWork.Save();

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
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVehicle(ulong id)
    {
        await _unitOfWork.Vehicles.DeleteVehicle(id);
        await _unitOfWork.Save();

        return Ok(BasicResponse.Successful);
    }
}