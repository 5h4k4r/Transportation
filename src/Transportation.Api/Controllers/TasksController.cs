using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tranportation.Api.Requests;
using Tranportation.Api.Responses;
using Transportation.Api.Common;
using Transportation.Api.Helpers;
using Transportation.Api.Interfaces;
using Transportation.Api.Models.Common;

namespace Tranportation.Api.Controllers;


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
    [ProducesResponseType(typeof(PaginatedResponse<ListTasksResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListTasks([FromQuery] ListTasksRequest model, [FromServices] UserAuthContext authContext)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);


        var AuthId = authContext.GetAuthUser().Id;

        var user = await _unitOfWork.User.GetUserByAuthId(AuthId);

        if (user is null || !user.AreaId.HasValue)
            return NotFound();

        if ((await _unitOfWork.RoleUsers.GetRoleUserByUserId(user.Id))?.RoleId < 5)
            model.AreaId = user.AreaId.Value;

        var items = await _unitOfWork.Tasks.ListTasks(model);
        var count = await _unitOfWork.Tasks.CountTasks(model);

        if (items is null)
            return NotFound();

        return Ok(new PaginatedResponse<ListTasksResponse>(count, model, items));
    }
}