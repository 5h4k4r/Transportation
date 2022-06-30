using Infra.Entities;

namespace Infra.Interfaces;

public interface IBaseTypeRepository
{
    Task<BaseType?> GetBaseTypeById(ulong id);
}