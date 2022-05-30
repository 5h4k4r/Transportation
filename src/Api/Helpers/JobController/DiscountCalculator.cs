using System.Text.Json;
using Core.Interfaces;
using Core.Models.Base;

namespace Api.Helpers.JobController;

internal class DiscountCalculator
{
    private string _now;

    public DiscountCalculator()
    {
        _now = DateTime.Now.ToString("H:i:s");
    }

    public Task<object> Discount(DiscountDto discount, double amount, IUnitOfWork unitOfWork)
    {
        var offer = new OfferDiscount();
        return offer.Calculate(discount, amount,unitOfWork);
    }

    public object Info(double discount, double amount, string? discountData)
    {
        var disc = JsonSerializer.Deserialize<DiscountData>(discountData ?? "");
        // var offer = new OfferDiscount(discount, amount);
        return OfferDiscount.Info(amount, disc?.DiscountedAmount, disc?.Discount, disc?.Percent, disc?.Max);
    }

    // public double Code(user, string, price, service_area_type_id)
    // {
    //     var offer = new OfferCode(user);
    //     offer->set(string, price, service_area_type_id);
    //     return offer->calculate();
    // }
}
