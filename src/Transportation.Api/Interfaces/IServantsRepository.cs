using Transportation.Api.Repositories;
using Transportation.Api.Requests;
using Servant = Transportation.Api.Model.Servant;

namespace Transportation.Api.Interfaces;


public interface IServantsRepository
{

    Task<Servant?> GetServantById(ulong Id);
    Task<ServantPerformance?> GetServantPerformance(ServantPerformanceRequest model, int ServantId);
}