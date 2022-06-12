using System.Net.Mime;
using AutoMapper;
using Core.Models.Common;
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
        var services = await _unitOfWork.Services.ListServices();
        if (services == null || services.Count == 0)
            return NotFound(new BasicResponse("No services found"));


        return Ok(services);
    }
}