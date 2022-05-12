using Core.Models;
using Core.Requests;
using Infra.Repositories;

namespace Core.Interfaces;


public interface IServantsRepository
{

    Task<ServantDTO?> GetServantById(ulong Id);
    Task<ServantPerformance?> GetServantPerformance(ServantPerformanceRequest model, int ServantId, ulong ServantUserId);
}