using Core.Models;
using Infra.Repositories;
using Infra.Requests;

namespace Core.Interfaces;


public interface IServantsRepository
{

    Task<ServantDTO?> GetServantById(ulong Id);
    Task<ServantPerformance?> GetServantPerformance(ServantPerformanceRequest model, int ServantId, ulong ServantUserId);
}