using Tranportation.Api.Requests;
using Tranportation.Api.Responses;
namespace Transportation.Api.Interfaces;

public interface IServantWorkDaysRepository
{
    Task<List<ListServantsOnlineHistoryResponse>?> ListServantsOnlineHistory(ListServantsOnlineHistoryRequest model);
    Task<int> ListServantsOnlineHistoryCount(ListServantsOnlineHistoryRequest model);
    Task<ServantWorkDaysResponse> GetServantWorkDays(ulong ServantId, ServantWorkDaysRequest model);
    Task<int> GetServantWorkDaysCount(ulong ServantId, ServantWorkDaysRequest model);
}