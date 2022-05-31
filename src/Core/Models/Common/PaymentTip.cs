namespace Infra.Repositories;

public class PaymentTip
{
    public PaymentClient Client { get; set; } = new();
    public PaymentServant Servant { get; set; } = new();
}