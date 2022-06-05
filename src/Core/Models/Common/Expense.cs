using Infra.Repositories;

namespace Core.Models.Common;

public class Expense
{
    public PaymentAmount Amount { get; set; }
    public PaymentTip Tip { get; set; }

    public object client_expense { get; set; }
    public object servant_expense { get; set; }
}