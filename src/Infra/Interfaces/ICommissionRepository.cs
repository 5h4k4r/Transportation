using Infra.Entities;

namespace Infra.Interfaces;

public interface ICommissionRepository
{
    Task<Commission?> GetCommissionById(ulong id);
    Task<Commission> CreateCommission(Commission commission);
    Commission DeleteCommission(Commission commission);

    Commission UpdateCommission(Commission updatedCommission);
}