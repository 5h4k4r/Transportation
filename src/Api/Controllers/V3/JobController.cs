using System.Net.Mime;
using System.Text.Json;
using Api.Extensions;
using Api.Helpers;
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
        var servant_position = await _unitOfWork.Cache.GetLastPositionOnTask("onTaskServant" + task.Task.Id);

        var distanceCalculation = CalculateDistance(task);
        var taskModel = new ListTasks();

        if (distanceCalculation != null) taskModel.TaskDistanceAndDuration = distanceCalculation;


        var discountAndExpense = await CalculateExpense(task, User.UserId(), taskModel.Requester);

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

        return taskModel;
    }

    public TaskDistance? CalculateDistance(TaskWithDistanceMemberTaxiMeter taskDto)
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

    private Task CalculateExpense(TaskWithDistanceMemberTaxiMeter taskDto, ulong user, Requester requester)
    {
        var price = 0;
        price = taskDto.Task.Request?.UserPrice ?? taskDto.Task.Price;
        var offer = new DiscountCalculator();
        var discount_info = offer.info(0, price, taskDto.Task.Request.Discount);

        // balance = Price::getBalance(task, this.payment);
        //
        // this.payment.setExpense(task.price, task.request.discount, (int)optional(task.active_discount_code)
        //     .amount, 0, balance);
        // expense = this.payment.getExpense();
        // expense = price::expense(user, requester, task, discount_info, expense);
        // return array(discount_info, expense);
    }
}

internal class DiscountCalculator
{
    private string _now;

    public DiscountCalculator()
    {
        _now = DateTime.Now.ToString("H:i:s");
    }

    public double discount(DiscountDto discount, double amount)
    {
        var offer = new OfferDiscount();
        return offer.calculate(discount, amount);
    }

    public double info(double discount, double amount, string? discountData)
    {
        var disc = JsonSerializer.Deserialize<DiscountData>(discountData ?? "");
        var offer = new OfferDiscount(discount, amount);
        return offer.info(amount, disc?.DiscountedAmount, disc?.Discount, disc?.Percent, disc?.Max);
    }

    public double code(user, string, price, service_area_type_id)
    {
        var offer = new OfferCode(user);
        offer->set(string, price, service_area_type_id);
        return offer->calculate();
    }
}

internal class OfferDiscount
{
    private double amount;
    private DiscountDto? discount;
    private readonly TimeOnly now;

    public OfferDiscount()
    {
        now = TimeOnly.FromDateTime(DateTime.Now);
    }

    public OfferDiscount(double discount, double amount)
    {
        now = TimeOnly.FromDateTime(DateTime.Now);
        this.discount = discount;
        this.amount = amount;
    }


    public double calculate(DiscountDto discount, double amount, IUnitOfWork unitOfWork)
    {
        this.discount = discount;
        this.amount = amount;

        var discounted_amount = this.amount;
        var amount_of_discount = 0;
        var percent = 0;

        if (this.discount != null && IsDiscountTime())
        {
            var isParsed = Enum.TryParse<Currency>(this.discount.ServiceAreaType.Currency, out var karat);
            amount_of_discount = Helpers.Helpers.RoundPrice(
                Helpers.Helpers.percentage(this.amount, this.discount.Value), karat,
                "toDown", unitOfWork);

            if (amount_of_discount > this.discount.Max)
                amount_of_discount = this.discount.Max;

            discounted_amount = this.amount - amount_of_discount;

            percent = this.discount.Value * 100;
        }

        return info(this.amount, discounted_amount, amount_of_discount, percent, this.discount?.Max);
    }

    private bool IsDiscountTime()
    {
        var jsonSerializerOptions = new JsonSerializerOptions();
        jsonSerializerOptions.Converters.Add(new JsonTimeOnlyConverter());
        var data = JsonSerializer
            .Deserialize<List<List<TimeOnly>>>(discount?.Periods ?? string.Empty, jsonSerializerOptions);
        var count = data?.Count ?? 0;
        return count switch
        {
            1 => now >= data![0][0] && now <= data![0][1],
            2 => (now >= data![0][0] && now <= data![0][1]) ||
                 (now >= data![1][0] && now <= data![1][1]),
            _ => false
        };
    }

    public static object info(double amount, double discounted_amount, int amount_of_discount,
        double percent,
        double? max)
    {
        return new
        {
            RealAmount = amount,
            DiscountedAmount = discounted_amount,
            Discount = amount_of_discount,
            Percent = percent,
            Max = max,
            Info = new
            {
                // TODO: Implement Translations | text = trans('content.discount'),
                text = "content.discount",
                value = percent,
                setting =
                    new
                    {
                        color = @"#101010",
                        background_color = @"#FFFFFF",
                        bold = true,
                    }
            }
        };
    }
}