using System.Net.Mime;
using System.Text.Json;
using Api.Extensions;
using Core.Constants;
using Core.Interfaces;
using Core.Models.Base;
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
[Route("v3/client/job")]
public class JobController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public JobController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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
                            return Ok(RidingClientPosition(userId.ToString(), activeTask, jobRequest.Origin.Latitude,
                                jobRequest.Origin.Longitude));

                        break;
                    case "Delivery":
                        return BadRequest(new BasicResponse("You have an active task, please complete it first"));
                }
            }
        }

        return BadRequest(new BasicResponse("You have an active task, please complete it first"));
    }

    public async Task<TaskDto> RidingClientPosition(
        string user,
        TaskWithDistanceMemberTaxiMeter task,
        double lat,
        double lng,
        double bearing = 0
    )
    {
        var servant_position = await _unitOfWork.Cache.GetLastPositionOnTask("onTaskServant" + task.Task);

        var distanceCalculation = CalculateDistance(task);

        var taskModel = new ListTasks();

        taskModel.Distance = distanceCalculation.distance;
        taskModel.duration = distanceCalculation.duration;

        var (discount, expense) = await CalculateExpense(taskModel, user, taskModel.requester.user);

        taskModel.price = discount.DiscountedAmount;
        if (taskModel.active_discount_code != null)
            taskModel.price -= (int)taskModel.active_discount_code.amount;

        taskModel.setDestinations();

        taskModel.setMember(user, servant_position.latitude, servant_position.longitude);

        var tips = await _unitOfWork.Cache.GetKey<string>("tip_object_" + task);
        if (tips != null) tips = JsonSerializer.Deserialize<Dictionary<string, object>>(tips);

        taskModel.tips = tips;

        taskModel.offer = discount.info;

        taskModel.two_way = taskModel.request.two_way;

        if (taskModel.status == JobStatus.TaskStatus.Stop || taskModel.status == JobStatus.TaskStatus.EndDestination)
        {
            var stopData = await TaskModel.GetStopData(taskModel);
            taskModel.stop = new Dictionary<string, object>
            {
                {
                    "price",
                    stopData.price
                },
                {
                    "time",
                    stopData.stop_duration
                },
                {
                    "start_time",
                    stopData.start_time
                }
            };
        }

        await TaskModel.SetServantDetail(taskModel);

        await TaskModel.SetServantPosition(taskModel);

        taskModel.price = Helpers.RoundPrice(taskModel.price, taskModel.request.service_area_type.currency);
        taskModel.expense = expense.Client.info;

        taskModel.chat = new Dictionary<string, object>
        {
            {
                "channelId",
                taskModel.message.chat_id
            }
        };

        await Course.Info(taskModel);
        await ChangeDestination.Info(taskModel);

        await _unitOfWork.Cache.AddList("onTaskClient" + task, lat + "_" + lng + "_" + bearing);

        return taskModel;
    }

    public TaskDistance CalculateDistance(TaskWithDistanceMemberTaxiMeter taskDto)
    {
        var successDestinations = taskDto.DestinationDtos;

        if (successDestinations != null)
        {
            var destinations = successDestinations as DestinationDto[] ?? successDestinations.ToArray();
            var distance = (ulong?)destinations.Sum(x => x.Distance);
            var duration = (ulong?)destinations.Sum(x => x.Duration);
            if (taskDto.TaxiMeter == null)
                return new TaskDistance()
                {
                    Distance = distance,
                    Duration = duration,
                };

            return new TaskDistance()
            {
                Distance = distance + taskDto.TaxiMeter.Distance,
                Duration = duration + taskDto.TaxiMeter.Duration,
            };
        }

        return null;
    }
    
    void CalculateExpense(TaskWithDistanceMemberTaxiMeter taskDto, User user, requester)
    {
        if ($task->request->user_price) {
            $price = $task->request->user_price;
        } else {
            $price = $task->price;
        }

        $discount_info = $this->offer->info(0, $price, $task->request->discount);

        $balance = Price::getBalance($task, $this->payment);

        $this->payment->setExpense($task->price, $task->request->discount, (int)optional($task->active_discount_code)->amount, 0, $balance);
        $expense = $this->payment->getExpense();

            $expense = price::expense($user, $requester, $task, $discount_info, $expense);
        return array($discount_info, $expense);
    }
}