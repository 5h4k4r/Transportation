using Core.Helpers.Job;
using Core.Models.Base;
using Core.Models.Common;

namespace Core.Interfaces;

public interface IPaymentRepository
{
    public Task<string?> DefaultAccount(UserDto user, string paymentCurrency);

    public void SetExpense(double price, DiscountData requestDiscount, int discount_code, int gifts_to_pay,
        int balance = 0);

    public Task<Expense> GetExpense(IEnumerable<string>? toAccounts);
}