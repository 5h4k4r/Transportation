using Core.Models.Base;
using Core.Models.Repositories;
using Core.Models.Requests;
using Core.Models.Responses;
using Infra.Entities;

namespace Infra.Interfaces;

public interface IServantsRepository
{
    Task<ServantDto?> GetServantByUserId(ulong userId, ulong areaId = 0);
    Task<ServantPerformance?>
        GetServantPerformance(GetServantPerformanceRequest model, int servantId, ulong servantUserId);

    Task<List<ListServantsPerformances>> ListServantPerformances(ListServantPerformancesRequest model);
    Task<List<ServantDto>> ListServants(ListServantRequest model, ulong userAreaId);
    Task<int> ListServantsCount(ListServantRequest model, ulong userAreaId);
    Task<Servant> CreateServant(ServantDto servant);
    Task<Servant> UpdateServant(UpdateServantRequest model, ulong userId);
    Task<ServantDto> DeleteServant(ServantDto servant);
    Task<TaskDto?> ServantActiveTask(ulong id);
    Task<ListServantsWithTheirStatusesResponse> ListServantsWithTheirStatuses(ulong areaId,
        ListServantsWithTheirStatusesRequest status);
    Task<ServiceResponse?> GetServantsServices(ulong id, ulong langId);
}