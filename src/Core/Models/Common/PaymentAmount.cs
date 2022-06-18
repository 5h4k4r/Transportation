using Infra.Repositories;

namespace Core.Models.Common;

public class PaymentAmount
{
    public PaymentClient Client { get; set; } = new();
    public PaymentServant Servant { get; set; } = new();
    public long? cash { get; set; }
    public long? credit { get; set; }
}