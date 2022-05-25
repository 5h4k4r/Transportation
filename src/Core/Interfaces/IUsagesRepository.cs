using Core.Models.Base;
using Core.Models.Requests;

namespace Core.Interfaces;

public interface IUsagesRepository
{
    Task<List<UsageDto>> ListUsages();
    Task CreateUsage(CreateUsageRequest model);
    Task<UsageDto?> GetUsageById(ulong id);
}