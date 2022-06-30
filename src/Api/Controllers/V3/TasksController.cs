using System.Net.Mime;
using Api.Extensions;
using Core.Models.Authentication;
using Core.Models.Common;
using Core.Models.Exceptions;
using Core.Models.Repositories;
using Core.Models.Requests;
using Infra.Authentication;
using Infra.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.V3;

[Authorize]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[Route("v3/tasks")]
public class TasksController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public TasksController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    ///     Lists all the tasks.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(PaginatedResponse<ListTasks>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListTasks([FromQuery] ListTasksRequest model,
        [FromServices] UserAuthContext authContext)
    {
        if (!User.HasRole(Role.SuperAdmin) && !User.GetAreaId().HasValue)
            throw new UnauthorizedException();

        model.AreaId = User.GetAreaId()!.Value;

        // if ((await _unitOfWork.RoleUsers.GetRoleUserByUserId(user.Id))?.RoleId < 5)
        //     model.AreaId = user.AreaId.Value;

        var items = await _unitOfWork.Tasks.ListTasks(model);
        var count = await _unitOfWork.Tasks.CountTasks(model);

        return Ok(new PaginatedResponse<ListTasks>(count, model, items));
    }

    /// <summary>
    ///     Lists all tasks by a client.
    /// </summary>
    [HttpGet("client/{clientId}")]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(PaginatedResponse<ListTasksByClientResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListTasksByClient(ulong clientId, [FromQuery] ListTasksByClientRequest model)
    {
        var items = await _unitOfWork.Tasks.ListTasksByClient(clientId, model);
        var count = await _unitOfWork.Tasks.CountClientTasks(clientId, model);

        return Ok(new PaginatedResponse<ListTasksByClientResponse>(count, model, items));
    }
}