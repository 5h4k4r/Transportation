using Core.Models;
using Core.Requests;
using Infra.Repositories;

namespace Core.Interfaces;


public interface IServantsRepository
{

    Task<ServantDTO?> GetServantById(ulong Id, ulong AreaId);
    Task<ServantPerformance?> GetServantPerformance(ServantPerformanceRequest model, int ServantId, ulong ServantUserId);
    Task<List<ServantDTO>> ListServants(ListServantRequest model, ulong UserAreaId);

    void CreateServant(ServantDTO servant);
}