using System.Text.Json;
using Core.Interfaces;
using Core.Models.Base;
using Core.Models.Common;

namespace Api.Helpers.JobController;


internal class OfferDiscount
{
    private readonly TimeOnly _Now;
    private double _Amount;
    private DiscountDto? _Discount;

    public OfferDiscount()
    {
        _Now = TimeOnly.FromDateTime(DateTime.Now);
    }

    public OfferDiscount(double discount, double amount)
    {
        _Now = TimeOnly.FromDateTime(DateTime.Now);
        // _Discount = discount;
        _Amount = amount;
    }


    public async Task<object> Calculate(DiscountDto discount, double amount, IUnitOfWork unitOfWork)
    {
        _Discount = discount;
        _Amount = amount;

        var discountedAmount = _Amount;
        var amountOfDiscount = 0.0;
        var percent = 0.0;

        if (_Discount != null && IsDiscountTime())
        {
            var isParsed = Enum.TryParse<Currency>(_Discount.ServiceAreaType.Currency, out var karat);
            amountOfDiscount = await Core.Helpers.Helpers.RoundPrice(
                Core.Helpers.Helpers.percentage(_Amount, _Discount.Value), karat,
                "toDown", unitOfWork);

            if (amountOfDiscount > _Discount.Max)
                amountOfDiscount = _Discount.Max;

            discountedAmount = _Amount - amountOfDiscount;

            percent = _Discount.Value * 100;
        }

        return Info(_Amount, discountedAmount, amountOfDiscount, percent, _Discount?.Max);
    }

    private bool IsDiscountTime()
    {
        var jsonSerializerOptions = new JsonSerializerOptions();
        jsonSerializerOptions.Converters.Add(new JsonTimeOnlyConverter());
        var data = JsonSerializer
            .Deserialize<List<List<TimeOnly>>>(_Discount?.Periods ?? string.Empty, jsonSerializerOptions);
        var count = data?.Count ?? 0;
        return count switch
        {
            1 => _Now >= data![0][0] && _Now <= data![0][1],
            2 => (_Now >= data![0][0] && _Now <= data![0][1]) ||
                 (_Now >= data![1][0] && _Now <= data![1][1]),
            _ => false
        };
    }

    public static object Info(double amount, double? discountedAmount, double? amountOfDiscount,
        double? percent,
        double? max)
    {
        return new
        {
            RealAmount = amount,
            DiscountedAmount = discountedAmount,
            Discount = amountOfDiscount,
            Percent = percent,
            Max = max,
            Info = new
            {
                // TODO: Implement Translations | text = trans('content.discount'),
                text = "content.discount",
                value = percent,
                setting =
                    new
                    {
                        color = @"#101010",
                        background_color = @"#FFFFFF",
                        bold = true
                    }
            }
        };
    }
}