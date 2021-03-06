using System.Net.Mime;
using Core.Interfaces;
using Core.Models;
using Infra.Entities.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;


[Authorize]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[Route("v3/roles")]
public class RolesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public RolesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [ProducesResponseType(typeof(List<RoleDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [HttpGet("{type}")]
    public async Task<ActionResult> ListRolesByType(sbyte type)
    {
        var roles = await _unitOfWork.Roles.ListRoleByTtpe(type);
        if (roles is null)
            return NotFound();

        return Ok(roles);
    }



}