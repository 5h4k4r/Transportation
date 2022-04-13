using Transportation.Api.Repositories;
using Transportation.Api.Requests;
using Servant = Transportation.Api.Model.Servant;

namespace Transportation.Api.Interfaces;


public interface IServantsPerformanceRepository
{

    Task<Servant?> GetServantById(int Id);
    Task<ServantPerformance?> GetServantPerformance(ServantPerformanceRequest model);
}