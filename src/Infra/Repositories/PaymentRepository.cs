using Core.Helpers;
using Core.Interfaces;
using Core.Models.Base;
using Core.Models.Common;

namespace Infra.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly ICurl _curl;

    public List<KeyValuePair<string, int>> _Cost = new()
    {
        new KeyValuePair<string, int>("cash", 0),
        new KeyValuePair<string, int>("credit", 0),
    };

    /**
     * @var array
     */
    public List<int> amount = new(), tip = new();

    public int commission;

    private string curl, payment_url;

    // private readonly int headers;
    private string api_token;
    public int currency;
    public int discount;
    public int discount_code;
    private int factor;
    public List<int> factor_requests;

    public int gifts_to_pay;
    //     [
    // "cash" => 0,
    // "credit" => 0
    // ];

    public int price = 0;
    public int reason;

    public List<int> result = new();
    public int should_pay = 0, can_pay = 0, payed_commission = 0;
    public int task;
    public List<int> transaction_data;

    public PaymentRepository(ICurl curl)
    {
        _curl = curl;
    }

    AccountDto defaultAccount(UserDto user, Currency currency)
    {
        var url = payment_url + "/service/accounts/default";
        var headers = new List<KeyValuePair<string, string>>()
            { new("api-token:", api_token) };

        var fields = new List<KeyValuePair<string, string>>()
        {
            new("user_id", user.AuthId),
            new("currency", Helpers.CurrencySymbol(currency)),
        };

        var data = _curl.Send<dynamic>(url, true, true, fields, headers);

        if (data["code"] != null && data["code"] == 200) {
            account = data["data"]["account"]["account_number"];
        } else {
            account = 0;
        }
        return account;
    }
}