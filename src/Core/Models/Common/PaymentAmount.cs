namespace Infra.Repositories;

public class PaymentAmount
{
    public PaymentClient Client { get; set; } = new();
    public PaymentServant Servant { get; set; } = new();
    public double? cash { get; set; }
    public double? credit { get; set; }
}