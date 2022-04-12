using Transportation.Api.Model;
using Transportation.Api.Requests;
using Transportation.Api.Responses;

namespace Transportation.Api.Interfaces;


public interface IServantsPerformanceRepository
{

    Task<Servant?> GetServantById(int Id);
    Task<ServantPerformanceResponse?> ServantPerformance(ServantPerformanceRequest model);
}