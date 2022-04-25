using Tranportation.Api.Requests;
using Tranportation.Api.Responses;
namespace Transportation.Api.Interfaces;

public interface IServantWorkDaysRepository
{
    Task<List<ListServantsOnlineHistoryResponse>> ListServantsOnlineHistory(ServantOnlinePeriodRequest model);
    Task<ServantWorkDaysResponse> GetServantWorkDays(ulong ServantId, ServantOnlinePeriodRequest model);
}