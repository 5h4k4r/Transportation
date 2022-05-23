
using System.Net.Mime;
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

    public VehiclesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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

    [ProducesResponseType(typeof(PaginatedResponse<UserDTO>), StatusCodes.Status200OK)]
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

    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<IActionResult> CreateVehicle([FromBody] CreateVehicleRequest request)
    {
        var vehicle = _unitOfWork.Vehicles.AddVehicle(request);
        var vehicleDetail = _unitOfWork.Vehicles.AddVehicleDetail(request);
        return Ok(BasicResponse.Successful);
    }


}