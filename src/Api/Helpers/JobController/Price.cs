using System.Text.Json;
using Core.Interfaces;
using Core.Models.Common;

namespace Api.Helpers.JobController;

class BalanceModel
{
    public long balance { get; set; }
}

public class Price
{
    private const int CashAccount = 0;
    private const int DefaultSystemAccount = 100;

    public static async Task<object> getBalance(TaskWithDistanceMemberTaxiMeter taskDto, Payment payment,
        IUnitOfWork unitOfWork)
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
                    var account = unitOfWork.Cache.GetKey("user_default_account_" + member.UserId);
                    if (!account)
                    {
                        account =  payment.defaultAccount(task.requester.user, task.request.service_area_type.currency);
                        if (account)
                        {
                            RedisManager::setKey("user_default_account_".member.user_id, account, 600);
                        }
                    }

                    member.payment_type.type = account;
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
}