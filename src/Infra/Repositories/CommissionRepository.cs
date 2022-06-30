using AutoMapper;
using Infra.Entities;
using Infra.Interfaces;

namespace Infra.Repositories;

public class CommissionRepository : ICommissionRepository
{
    private readonly TransportationContext _context;
    private readonly IMapper _mapper;

    public CommissionRepository(TransportationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Commission?> GetCommissionById(ulong id)
    {
        var commission = await _context.Commissions.FindAsync(id);
        return commission;
    }

    public async Task<Commission> CreateCommission(Commission newCommission)
    {
        var commissions = await _context.Commissions.AddAsync(newCommission);
        return commissions.Entity;
    }

    public Commission UpdateCommission(Commission updatedCommission)
    {
        var commissions = _context.Commissions.Update(updatedCommission);
        return commissions.Entity;
    }

    public Commission DeleteCommission(Commission commission)
    {
        commission.DeletedAt = DateTime.UtcNow;
        var updatedCommission = _context.Commissions.Update(commission);
        return updatedCommission.Entity;
    }
}