using Core.Models.Base;
using Core.Models.Repositories;
using Core.Models.Requests;

namespace Core.Interfaces;


public interface IServantsRepository
{

    Task<ServantDto?> GetServantById(int id, ulong areaId);
    Task<ServantPerformance?> GetServantPerformance(ServantPerformanceRequest model, int servantId, ulong servantUserId);
    Task<List<ListServants>> ListServants(ListServantRequest model, ulong userAreaId);
    Task<int> ListServantsCount(ListServantRequest model, ulong UserAreaId);
    void CreateServant(ServantDto servant);
}