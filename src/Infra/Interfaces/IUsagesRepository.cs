using Core.Models.Base;
using Core.Models.Requests;

namespace Infra.Interfaces;

public interface IUsagesRepository
{
    Task<List<UsageDto>> ListUsages();
    Task CreateUsage(CreateUsageRequest model);
    Task<UsageDto?> GetUsageById(ulong id);
}