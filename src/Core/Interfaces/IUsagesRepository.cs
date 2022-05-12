using Core.Models;

namespace Core.Repositories;

public interface IUsagesRepository
{
    Task<List<UsageDTO>> ListUsages();
    Task<UsageDTO> CreateUsage(CreateUsageRequest model);
    Task<UsageDTO?> GetUsageById(ulong Id);
}