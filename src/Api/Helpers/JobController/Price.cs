using Core.Interfaces;
using Core.Models.Common;
using ServiceStack.Text;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Api.Helpers.JobController;

internal class BalanceModel
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
                            await unitOfWork.Cache.SetKey("user_default_account_" + member.UserId, account,
                                TimeSpan.FromSeconds(600));
                    }

                    member.MemberPaymentTypes.First().Type = account;
                }

                balance = await payment.GetBalance(member.MemberPaymentTypes.First().Type);
                balance = Core.Helpers.Helpers.Exchange(balance,
                    Enum.Parse<Currency>(taskDto.Task.Request.ServiceAreaType.Currency));
                balance = await Core.Helpers.Helpers.RoundPrice(balance,
                    Enum.Parse<Currency>(taskDto.Task.Request.ServiceAreaType.Currency), "toDown", unitOfWork);
                await unitOfWork.Cache.SetKey("lastTimeGetBalance_" + taskDto.Task.Id, JsonSerializer.Serialize(new
                {
                    balance,
                    time = DateTime.UtcNow.ToUnixTime()
                }), TimeSpan.FromSeconds(60));
            }
        }

        return balance;
    }

    public static async Task<Expense> Expense(TaskWithDistanceMemberTaxiMeter taskDto, Expense expense,
        IUnitOfWork unitOfWork)
    {
        var currency = Enum.Parse<Currency>(taskDto.Task.Request.ServiceAreaType.Currency);
        var servantExpense = await ServantExpense(taskDto, currency, expense, unitOfWork);
        var clientExpense = await ClientExpense(taskDto, currency, expense, unitOfWork);

        return new Expense
        {
            client_expense = clientExpense,
            servant_expense = servantExpense
        };
    }

    public static async Task<object> ServantExpense(TaskWithDistanceMemberTaxiMeter task, Currency currency,
        Expense expense,
        IUnitOfWork unitOfWork)
    {
        var (cashDesc, creditDesc) = setDescription("servant", task, expense);

        return new
        {
            credit = new
            {
                tip = expense.Tip.Servant.credit,
                amount = expense.Amount.Servant.credit
            },
            cash = new
            {
                tip = expense.Tip.Servant.cash,
                amount = expense.Amount.Servant.cash
            },
            info = new object[]
            {
                new
                {
                    tip = expense.Tip.Servant.credit,
                    amount = expense.Amount.Servant.credit,
                    type = "CREDIT",
                    description = creditDesc,
                    setting = new
                    {
                        bold = false,
                        color = "#f21124"
                    }
                },


                new
                {
                    tip = expense.Tip.Servant.cash,
                    amount = await Core.Helpers.Helpers.RoundPrice(expense.Amount.Servant.cash ?? 0, currency, null,
                        unitOfWork),
                    type = "CASH",
                    description = cashDesc,
                    setting = new
                    {
                        bold =
                            true,
                        color = "#f21124"
                    }
                }
            }
        };
    }

    public static async Task<object> ClientExpense(TaskWithDistanceMemberTaxiMeter task, Currency currency,
        Expense expense,
        IUnitOfWork unitOfWork)
    {
        var (cashDesc, creditDesc) = setDescription("client", task, expense);

        return new
        {
            credit = new
            {
                tip = expense.Tip.Client.credit,
                amount = expense.Amount.Client.credit
            },
            cash = new
            {
                tip = expense.Tip.Client.cash,
                amount = expense.Amount.Client.cash
            },
            info = new object[]
            {
                new
                {
                    tip = expense.Tip.Client.credit,
                    amount = expense.Amount.Client.credit,
                    type = "CREDIT",
                    description = creditDesc,
                    setting = new
                    {
                        bold = false,
                        color = "#f21124"
                    }
                },


                new
                {
                    tip = expense.Tip.Client.cash,
                    amount = await Core.Helpers.Helpers.RoundPrice(expense.Amount.Client.cash ?? 0, currency, null,
                        unitOfWork),
                    type = "CASH",
                    description = cashDesc,
                    setting = new
                    {
                        bold = true,
                        color = "#f21124"
                    }
                }
            }
        };
    }

    public static (string, string) setDescription(string user_type, TaskWithDistanceMemberTaxiMeter task,
        Expense expense)
    {
        const int defaultSystem = 100;
        const int cash = 0;
        var requesterPaymentType = task.Requester?.PaymentType ?? "0";
        var cashDesc = "";
        var creditDesc = "";
        // TODO: Translate
        var payType = int.Parse(requesterPaymentType);
        if (payType == cash)
        {
            // cash_desc = trans("content.expense.cash");
            cashDesc = "content.expense.cash";
            creditDesc = "";
        }
        else
        {
            switch (expense.Amount.Servant.cash)
            {
                case > 0:
                    // credit_desc = trans("content.expense.balance_not_enough");
                    creditDesc = "content.expense.balance_not_enough";
                    cashDesc = "";
                    break;
                case 0:
                    // credit_desc = trans("content.expense.balance_enough.".user_type);
                    creditDesc = "content.expense.balance_enough." + user_type;
                    cashDesc = "";
                    break;
            }
        }

        return (cashDesc, creditDesc);
    }
}