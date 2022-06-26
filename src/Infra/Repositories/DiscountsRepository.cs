using AutoMapper;
using Infra.Entities;
using Infra.Interfaces;

namespace Infra.Repositories;

public class DiscountsRepository : IDiscountsRepository
{
    private readonly TransportationContext _context;

    private readonly IMapper _mapper;

    public DiscountsRepository(TransportationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Discount?> GetDiscountById(ulong id)
    {
        var discounts = await _context.Discounts.FindAsync(id);
        return discounts;
    }

    public async Task<Discount?> CreateDiscount(Discount discount)
    {
        var discounts = await _context.Discounts.AddAsync(discount);
        return discounts.Entity;
    }

    public Discount UpdateDiscount(Discount discount)
    {
        _context.Discounts.Update(discount);
        return discount;
    }
}