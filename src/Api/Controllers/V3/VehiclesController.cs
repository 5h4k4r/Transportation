using System.Net.Mime;
using AutoMapper;
using Core.Interfaces;
using Core.Models.Base;
using Core.Models.Common;
using Core.Models.Requests;
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
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

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

    [ProducesResponseType(typeof(VehicleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetVehicle(ulong id)
    {
        var vehicle = await _unitOfWork.Vehicles.GetVehicleById(id);
        if (vehicle is null)
            return NotFound(BasicResponse.ResourceNotFound);

        return Ok(vehicle);

    }

    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
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

        var newVehicleDetail = _mapper.Map<VehicleDetailDto>(request);
        newVehicleDetail.Vehicle = newVehicle;

        _unitOfWork.Vehicles.AddVehicleDetail(newVehicleDetail);

        await _unitOfWork.Save();

        return Ok(newVehicleDetail);
    }


}