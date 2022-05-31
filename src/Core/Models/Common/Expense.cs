using Infra.Repositories;

namespace Core.Models.Common;

public class Expense
{
    public PaymentAmount Amount { get; set; }
    public PaymentTip Tip { get; set; }

    public double client_expense { get; set; }
    public double servant_expense { get; set; }
}