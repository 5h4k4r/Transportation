using System.Net.Mime;
using Core.Interfaces;
using Core.Models.Common;
using Core.Models.Repositories;
using Core.Models.Requests;
using Infra.Authentication;
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
    /// Lists all the tasks.
    /// </summary>

    [HttpGet]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(PaginatedResponse<ListTasks>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListTasks([FromQuery] ListTasksRequest model, [FromServices] UserAuthContext authContext)
    {
        var authId = authContext.GetAuthUser().Id;

        var user = await _unitOfWork.User.GetUserByAuthId(authId);

        if (user is null || !user.AreaId.HasValue)
            return NotFound();

        if ((await _unitOfWork.RoleUsers.GetRoleUserByUserId(user.Id))?.RoleId < 5)
            model.AreaId = user.AreaId.Value;

        var items = await _unitOfWork.Tasks.ListTasks(model);
        var count = await _unitOfWork.Tasks.CountTasks(model);

        return Ok(new PaginatedResponse<ListTasks>(count, model, items));
    }
    /// <summary>
    /// Lists all tasks by a client.
    /// </summary>

    [HttpGet("client")]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(PaginatedResponse<ListTasksByClient>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListTasksByClient([FromQuery] ListTasksByClientRequest model)
    {
        var items = await _unitOfWork.Tasks.ListTasksByClient(model);
        var count = await _unitOfWork.Tasks.CountClientTasks(model);

        return Ok(new PaginatedResponse<ListTasksByClient>(count, model, items));
    }
}