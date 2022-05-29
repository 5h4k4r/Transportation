using Core.Models.Base;
using Core.Models.Repositories;
using Core.Models.Requests;

namespace Core.Interfaces;

public interface IServantsRepository
{
    Task<ServantDto?> GetServantById(int id, ulong areaId = 0);

    Task<ServantDto?> GetServantByUserId(int id, ulong areaId = 0);

    Task<ServantPerformance?>
        GetServantPerformance(ServantPerformanceRequest model, int servantId, ulong servantUserId);

    Task<List<ServantDto>> ListServants(ListServantRequest model, ulong userAreaId);
    Task<int> ListServantsCount(ListServantRequest model, ulong userAreaId);
    void CreateServant(ServantDto servant);
}