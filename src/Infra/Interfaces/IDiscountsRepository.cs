using Infra.Entities;

namespace Infra.Interfaces;

public interface IDiscountsRepository
{
    Task<Discount?> GetDiscountById(ulong id);
    Task<Discount?> CreateDiscount(Discount discount);
    Discount UpdateDiscount(Discount discount);
}