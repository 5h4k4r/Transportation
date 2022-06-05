using System.Net.Mime;
using Api.Extensions;
using Api.Helpers.JobController;
using Core.Interfaces;
using Core.Models.Common;
using Core.Models.Requests;
using Infra.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.V3;

[Authorize]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[Route("v3/client/job")]
public class JobController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPaymentRepository _payment;

    public JobController(IUnitOfWork unitOfWork, IPaymentRepository payment)
    {
        _unitOfWork = unitOfWork;
        _payment = payment;
    }


    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BasicResponse), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<IActionResult> NewRequest([FromBody] NewJobRequest jobRequest,
        [FromServices] UserAuthContext authContext)
    {
        var userId = User.UserId();
        if (jobRequest.SelectedOptionId != null)
        {
            var activeTask = await _unitOfWork.Tasks.GetActiveTaskByServiceId(userId,
                (uint)jobRequest.SelectedOptionId);
            if (activeTask != null)
            {
                var taskPerformer = activeTask.Task.Request.ServiceAreaType.Usage.StaticKey;

                switch (taskPerformer)
                {
                    case "Riding":
                        if (jobRequest.Origin != null)
                            return Ok(Riding.RidingClientPosition(
                                    userId.ToString(),
                                    User.UserId(), activeTask,
                                    jobRequest.Origin.Latitude,
                                    jobRequest.Origin.Longitude,
                                    _unitOfWork,
                                    _payment
                                )
                            );

                        break;
                    case "Delivery":
                        return BadRequest(new BasicResponse("You have an active task, please complete it first"));
                }
            }
        }

        return BadRequest(new BasicResponse("You have an active task, please complete it first"));
    }
}