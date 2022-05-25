using Core.Models.Base;
using Core.Models.Repositories;
using Core.Models.Requests;

namespace Core.Interfaces;


public interface IServantsRepository
{

    Task<ServantDto?> GetServantById(ulong id, ulong areaId);
    Task<ServantPerformance?> GetServantPerformance(ServantPerformanceRequest model, int servantId, ulong servantUserId);
    Task<List<ServantDto>> ListServants(ListServantRequest model, ulong userAreaId);

    void CreateServant(ServantDto servant);
}