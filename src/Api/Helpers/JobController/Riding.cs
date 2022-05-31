using System.Text.Json;
using Core.Helpers.Job;
using Core.Interfaces;
using Core.Models.Base;
using Core.Models.Common;
using Core.Models.Repositories;

namespace Api.Helpers.JobController;

public class Riding
{
    public static async Task<TaskDto> RidingClientPosition(
        string user,
        ulong userId,
        TaskWithDistanceMemberTaxiMeter task,
        double lat,
        double lng,
        IUnitOfWork unitOfWork,
        IPaymentRepository payment,
        double bearing = 0
    )
    {
        var servantPosition = await unitOfWork.Cache.GetLastPositionOnTask("onTaskServant" + task.Task.Id);

        var distanceCalculation = CalculateDistance(task);
        var taskModel = new ListTasks();

        if (distanceCalculation != null) taskModel.TaskDistanceAndDuration = distanceCalculation;


        var discountAndExpense = await CalculateExpense(task, userId, taskModel.Requester, unitOfWork, payment);

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

    public static TaskDistance? CalculateDistance(TaskWithDistanceMemberTaxiMeter taskDto)
    {
        var successDestinations = taskDto.DestinationDtos;

        if (successDestinations == null) return null;
        var destinations = successDestinations as DestinationDto[] ?? successDestinations.ToArray();
        var distance = (ulong?)destinations.Sum(x => x.Distance);
        var duration = (ulong?)destinations.Sum(x => x.Duration);
        if (taskDto.TaxiMeter == null)
            return new TaskDistance
            {
                Distance = distance,
                Duration = duration
            };

        return new TaskDistance
        {
            Distance = distance + taskDto.TaxiMeter.Distance,
            Duration = duration + taskDto.TaxiMeter.Duration
        };
    }

    private static async Task<Tuple<object, Expense>> CalculateExpense(TaskWithDistanceMemberTaxiMeter taskDto,
        ulong user, Requester requester,
        IUnitOfWork unitOfWork, IPaymentRepository payment)
    {
        var price = 0;
        price = taskDto.Task.Request?.UserPrice ?? taskDto.Task.Price;
        var offer = new DiscountCalculator();
        var discountInfo = offer.Info(0, price, taskDto.Task.Request.Discount);

        var balance = Price.getBalance(taskDto, unitOfWork, payment);

        var disc = JsonSerializer.Deserialize<DiscountData>(taskDto.Task.Request.Discount ?? "");
        payment.SetExpense(taskDto.Task.Price, disc, taskDto.DiscountCodeUser.Amount, 0);
        var expense = await payment.GetExpense(null);
        expense = Price.Expense(user, requester, taskDto, discountInfo, expense);
        return new Tuple<object, Expense>(discountInfo, expense);
        return null;
    }
}