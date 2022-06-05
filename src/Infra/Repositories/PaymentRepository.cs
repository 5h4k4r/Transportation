using System.Net;
using System.Text.Json;
using Core.Helpers.Job;
using Core.Interfaces;
using Core.Models.Base;
using Core.Models.Common;

namespace Infra.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly ICurl _curl;
    private readonly IUnitOfWork _unitOfWork;

    public List<KeyValuePair<string, int>> _Cost = new()
    {
        new KeyValuePair<string, int>("cash", 0),
        new KeyValuePair<string, int>("credit", 0)
    };

    /**
     * @var array
     */
    public PaymentAmount _PaymentAmount = new();

    public PaymentGift _PaymentGift = new();

    /**
     * @var array
     */
    public PaymentTip _PaymentTip = new();

    // private readonly int headers;
    private string api_token;

    public int commission;
    public int currency;
    public int discount;
    public int discount_code;
    private int factor;
    public List<KeyValuePair<string, dynamic>> factorRequests;

    public int gifts_to_pay;

    private string payment_url;
    //     [
    // "cash" => 0,
    // "credit" => 0
    // ];

    public int price = 0;
    public int reason;

    public List<int> result = new();
    public int should_pay = 0, can_pay = 0, payed_commission;
    public TaskDto? task;
    public List<int> transaction_data;

    public PaymentRepository(ICurl curl, IUnitOfWork unitOfWork)
    {
        _curl = curl;
        _unitOfWork = unitOfWork;
    }

    public async Task<string?> DefaultAccount(UserDto user, string paymentCurrency)
    {
        var url = payment_url + "/service/accounts/default";
        var headers = new List<KeyValuePair<string, string>> { new("api-token:", api_token) };

        var fields = new List<KeyValuePair<string, string>>
        {
            new("user_id", user.AuthId),
            new("currency", paymentCurrency)
        };

        var data = await _curl.Send<dynamic>(url, true, true, fields, headers);

        if (data?.code != null && data?.code == 200)
            return data?.data.account.account_number;
        return "0";
    }

    // TODO: REMOVE Double types for money, either use decimal or use uint
    public void SetExpense(double price, DiscountData requestDiscount, int discount_code, int gifts_to_pay,
        int balance = 0)
    {
        var priceWithDiscount = price - (requestDiscount.Discount + discount_code + gifts_to_pay);
        var clientPriceWithDiscount = requestDiscount.DiscountedAmount - (discount_code + gifts_to_pay);
        _PaymentAmount.Client.cash = (long)(clientPriceWithDiscount >= balance ? clientPriceWithDiscount - balance : 0);
        _PaymentAmount.Client.credit = (long?)(clientPriceWithDiscount >= balance ? balance : clientPriceWithDiscount);
        _PaymentAmount.Servant.cash = (long)(priceWithDiscount >= balance ? priceWithDiscount - balance : 0);
        _PaymentAmount.Servant.credit = (long?)(priceWithDiscount >= balance ? balance : priceWithDiscount);
        _PaymentAmount.cash = (long)(priceWithDiscount >= balance ? priceWithDiscount - balance : 0);
        _PaymentAmount.credit = (long?)(requestDiscount.Discount + discount_code + gifts_to_pay +
                                        (priceWithDiscount >= balance
                                            ? balance
                                            : priceWithDiscount));

        _PaymentTip.Client.cash = 0;
        _PaymentTip.Client.credit = 0;
        _PaymentTip.Servant.cash = 0;
        _PaymentTip.Servant.credit = 0;

        throw new NotImplementedException("REMOVE Double types for money, either use decimal or use uint");
    }


    public async Task<Expense> GetExpense(IEnumerable<string>? toAccounts)
    {
        if (task != null)
        {
            var expp = JsonSerializer.Deserialize<Expense>(
                await _unitOfWork.Cache.GetKey<string>($"ExpenseTask{task.Id}"));

            if (expp != null) return expp;
        }

        var dara = new List<string> { "DISCOUNT_TASK", "DISCOUNT_CODE", "GIFT" };

        foreach (var factorRequest in factorRequests)
            if (factorRequest.Value.code != null && factorRequest.Value.code == HttpStatusCode.OK)
            {
                if (string.Equals(factorRequest.Key, "TASK", StringComparison.OrdinalIgnoreCase))
                {
                    _PaymentAmount.Servant.credit += factorRequest.Value.transaction.amount;
                    _PaymentAmount.Servant.cash -= factorRequest.Value.transaction.amount;
                    _PaymentAmount.Client.credit += factorRequest.Value.transaction.amount;
                    _PaymentAmount.Client.cash -= factorRequest.Value.transaction.amount;
                }
                else if (string.Equals(factorRequest.Key, "TIP", StringComparison.OrdinalIgnoreCase))
                {
                    _PaymentTip.Servant.credit += factorRequest.Value.transaction.amount;
                    _PaymentTip.Servant.cash -= factorRequest.Value.transaction.amount;
                    _PaymentTip.Client.credit += factorRequest.Value.transaction.amount;
                    _PaymentTip.Client.cash -= factorRequest.Value.transaction.amount;
                }
                else if (dara.Contains(factorRequest.Key))
                {
                    _PaymentGift.willPay -= factorRequest.Value.transaction.amount;
                    _PaymentGift.paied += factorRequest.Value.transaction.amount;
                    _PaymentGift.detail[factorRequest.Key] = factorRequest.Value.transaction.amount;
                }
                else if (string.Equals(factorRequest.Key, "COMMISSION", StringComparison.OrdinalIgnoreCase))
                {
                    payed_commission = factorRequest.Value.transaction.amount;
                }
            }
            else
            {
                if (string.Equals(factorRequest.Key, "GIFT", StringComparison.OrdinalIgnoreCase))
                {
                    _PaymentAmount.Servant.credit -= gifts_to_pay;
                    _PaymentAmount.Servant.cash += gifts_to_pay;
                    _PaymentAmount.Client.credit -= gifts_to_pay;
                    _PaymentAmount.Client.cash += gifts_to_pay;
                }
                else if (string.Equals(factorRequest.Key, "DISCOUNT_TASK", StringComparison.OrdinalIgnoreCase))
                {
                    _PaymentAmount.Servant.credit -= discount;
                    _PaymentAmount.Servant.cash += discount;
                    _PaymentAmount.Client.cash += discount;
                }
                else if (string.Equals(factorRequest.Key, "DISCOUNT_CODE", StringComparison.OrdinalIgnoreCase))
                {
                    _PaymentAmount.Servant.credit -= discount_code;
                    _PaymentAmount.Servant.cash += discount_code;
                    _PaymentAmount.Client.credit -= discount_code;
                    _PaymentAmount.Client.cash += discount_code;
                }
            }


        return new Expense
        {
            Amount = _PaymentAmount,
            Tip = _PaymentTip
        };
    }

    public async Task<long> GetBalance(string account_number)
    {
        var url = payment_url + "/service/accounts/amount";
        var headers = new List<KeyValuePair<string, string>> { new("api-token:", api_token) };

        var fields = new List<KeyValuePair<string, string>>
        {
            new("account_number", account_number)
        };

        var data = await _curl.Send<dynamic>(url, true, true, fields, headers);

        if (data?.code != null && data?.code == 200)
            return data?.data.account.account > 0 ? data?.data.account.account : 0;

        return 0;
    }
}