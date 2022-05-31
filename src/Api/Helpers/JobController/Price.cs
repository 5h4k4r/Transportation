using System.Text.Json;
using Core.Interfaces;
using Core.Models.Base;
using Core.Models.Common;
using Core.Models.Repositories;

namespace Api.Helpers.JobController;

class BalanceModel
{
    public long balance { get; set; }
}

public class Price
{
    private const int CashAccount = 0;
    private const int DefaultSystemAccount = 100;

    public static async Task<object> getBalance(TaskWithDistanceMemberTaxiMeter taskDto,
        IUnitOfWork unitOfWork, IPaymentRepository payment)
    {
        var member = taskDto.Member;
        long balance = 0;
        var lastTimeGetBalance =
            JsonSerializer.Deserialize<BalanceModel>(
                await unitOfWork.Cache.GetKey<string>("lastTimeGetBalance_" + taskDto.Task.Id));

        if (lastTimeGetBalance != null)
        {
            balance = lastTimeGetBalance.balance;
        }

        else
        {
            if (member.MemberPaymentTypes.Count != 0
                && member.MemberPaymentTypes.First().Type != CashAccount.ToString())
            {
                if (member.MemberPaymentTypes.First().Type == DefaultSystemAccount.ToString())
                {
                    var account = await unitOfWork.Cache.GetKey<string?>("user_default_account_" + member.UserId);
                    if (account == null)
                    {
                        account = await payment.DefaultAccount(taskDto.Member.User,
                            taskDto.Task.Request.ServiceAreaType.Currency);
                        if (account != null)
                        {
                            await unitOfWork.Cache.SetKey("user_default_account_" + member.UserId, account,
                                TimeSpan.FromSeconds(600));
                        }
                    }

                    member.MemberPaymentTypes.First().Type = account;
                }

                balance = payment.getBalance(member.payment_type.type);
                balance = Helpers::exchange(balance, task.request.service_area_type.currency);
                balance = Helpers::RoundPrice(balance, task.request.service_area_type.currency, "toDown");
                RedisManager::setKey("lastTimeGetBalance_".task.id, json_encode([
                    "balance" => balance,
                "time" => time(),
                    ]), 60);
            }
        }

        return balance;
    }

    public static Expense Expense(ulong servant, Requester client, TaskWithDistanceMemberTaxiMeter taskDto,
        object discountInfo, Expense expense)
    {
        var gift_amount = 0;
        var currency = taskDto.Task.Request.ServiceAreaType.Currency;
        var servant_expense = servantExpense(taskDto, servant, currency, expense);
        var client_expense = clientExpense(client, taskDto, currency, discountInfo, expense);

        return new Expense()
        {
            client_expense = client_expense,
            servant_expense = servant_expense,
        };
    }
}