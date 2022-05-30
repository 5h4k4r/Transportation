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
        double bearing = 0
    )
    {
        var servantPosition = await unitOfWork.Cache.GetLastPositionOnTask("onTaskServant" + task.Task.Id);

        var distanceCalculation = CalculateDistance(task);
        var taskModel = new ListTasks();

        if (distanceCalculation != null) taskModel.TaskDistanceAndDuration = distanceCalculation;


        var discountAndExpense = await CalculateExpense(task, userId, taskModel.Requester);

        // taskModel.price = discount.DiscountedAmount;
        // if (taskModel.active_discount_code != null)
        //     taskModel.price -= (int)taskModel.active_discount_code.amount;
        //
        // taskModel.setDestinations();
        //
        // taskModel.setMember(user, servant_position.latitude, servant_position.longitude);
        //
        // var tips = await _unitOfWork.Cache.GetKey<string>("tip_object_" + task);
        // if (tips != null) tips = JsonSerializer.Deserialize<Dictionary<string, object>>(tips);
        //
        // taskModel.tips = tips;
        //
        // taskModel.offer = discount.info;
        //
        // taskModel.two_way = taskModel.request.two_way;
        //
        // if (taskModel.status == JobStatus.TaskStatus.Stop || taskModel.status == JobStatus.TaskStatus.EndDestination)
        // {
        //     var stopData = await TaskModel.GetStopData(taskModel);
        //     taskModel.stop = new Dictionary<string, object>
        //     {
        //         {
        //             "price",
        //             stopData.price
        //         },
        //         {
        //             "time",
        //             stopData.stop_duration
        //         },
        //         {
        //             "start_time",
        //             stopData.start_time
        //         }
        //     };
        // }
        //
        // await TaskModel.SetServantDetail(taskModel);
        //
        // await TaskModel.SetServantPosition(taskModel);
        //
        // taskModel.price = Helpers.RoundPrice(taskModel.price, taskModel.request.service_area_type.currency);
        // taskModel.expense = expense.Client.info;
        //
        // taskModel.chat = new Dictionary<string, object>
        // {
        //     {
        //         "channelId",
        //         taskModel.message.chat_id
        //     }
        // };
        //
        // await Course.Info(taskModel);
        // await ChangeDestination.Info(taskModel);
        //
        // await _unitOfWork.Cache.AddList("onTaskClient" + task, lat + "_" + lng + "_" + bearing);

        // return taskModel;
        return null;
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

    private static Task CalculateExpense(TaskWithDistanceMemberTaxiMeter taskDto, ulong user, Requester requester, IUnitOfWork unitOfWork)
    {
        var price = 0;
        price = taskDto.Task.Request?.UserPrice ?? taskDto.Task.Price;
        var offer = new DiscountCalculator();
        var discountInfo = offer.Info(0, price, taskDto.Task.Request.Discount);

        var balance = Price.getBalance(taskDto, this.payment);
        //
        // this.payment.setExpense(task.price, task.request.discount, (int)optional(task.active_discount_code)
        //     .amount, 0, balance);
        // expense = this.payment.getExpense();
        // expense = price::expense(user, requester, task, discount_info, expense);
        // return array(discount_info, expense);
        return null;
    }
}