using System.Net.Mime;
using Core.Models.Base;
using Core.Models.Common;
using Core.Models.Requests;
using Infra.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.V3;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[ProducesResponseType(typeof(BasicResponse), StatusCodes.Status401Unauthorized)]
[Route("v3/usages")]
[Authorize]
public class UsagesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public UsagesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    [ProducesResponseType(typeof(List<UsageDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [HttpGet]
    public async Task<IActionResult> ListUsages()
    {
        var usages = await _unitOfWork.Usages.ListUsages();


        return Ok(usages);
    }

    [ProducesResponseType(typeof(List<UsageDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUsageById([FromQuery] ulong id)
    {
        var usage = await _unitOfWork.Usages.GetUsageById(id);

        if (usage is null)
            return NotFound(BasicResponse.ResourceNotFound);

        return Ok(usage);
    }

    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [HttpPost]
    public async Task<IActionResult> CreateUsage(CreateUsageRequest model)
    {
        await _unitOfWork.Usages.CreateUsage(model);

        await _unitOfWork.Save();

        return Ok(BasicResponse.Successful);
    }
}