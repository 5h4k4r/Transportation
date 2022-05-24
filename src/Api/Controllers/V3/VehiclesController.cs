
using System.Net.Mime;
using AutoMapper;
using Core.Common;
using Core.Interfaces;
using Core.Models;
using Core.Requests;
using Infra.Entities.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

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

    [ProducesResponseType(typeof(PaginatedResponse<VehicleDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [HttpGet]
    public async Task<IActionResult> ListVehicles([FromQuery] ListVehiclesRequest model)
    {
        var Vehicle = await _unitOfWork.Vehicles.ListVehicle(model);
        var vehicelsCount = await _unitOfWork.Vehicles.ListVehicleCount(model);
        return Ok(new PaginatedResponse<VehicleDTO>(vehicelsCount, model, Vehicle));
    }

    [ProducesResponseType(typeof(VehicleDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetVehicle(ulong id)
    {
        var Vehicle = _unitOfWork.Vehicles.GetVehicleById(id);
        if (Vehicle is null)
            return NotFound(BasicResponse.ResourceNotFound);

        return Ok(Vehicle);

    }

    [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [HttpGet("crews/{id}")]
    public async Task<IActionResult> GetVehicleCrew(ulong id, [FromQuery] GetVehicleCrewRequest request)

    {
        List<UserDTO> Crew;
        if (request.VehicleCrew.Equals("Owner"))
            Crew = await _unitOfWork.Vehicles.GetVehicleOwners(id);
        else
            Crew = await _unitOfWork.Vehicles.GetVehicleUsers(id);


        if (Crew is null)
            return NotFound(BasicResponse.ResourceNotFound);


        return Ok(Crew);
    }

    [ProducesResponseType(typeof(VehicleDetailDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<IActionResult> CreateVehicle([FromBody] CreateVehicleRequest request)
    {
        if (request is null)
        {
            return BadRequest();
        }

        var newVehicle = _mapper.Map<VehicleDTO>(request);

        var newVehicleDetail = _mapper.Map<VehicleDetailDTO>(request);
        newVehicleDetail.Vehicle = newVehicle;

        _unitOfWork.Vehicles.AddVehicleDetail(newVehicleDetail);

        await _unitOfWork.Save();

        return Ok(newVehicleDetail);
    }


}