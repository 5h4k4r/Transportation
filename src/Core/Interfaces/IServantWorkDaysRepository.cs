
using Infra.Repositories;
using Infra.Requests;

namespace Core.Interfaces;

public interface IServantWorkDaysRepository
{
    Task<List<ListServantsOnlineHistory>?> ListServantsOnlineHistory(ListServantsOnlineHistoryRequest model);
    Task<int> ListServantsOnlineHistoryCount(ListServantsOnlineHistoryRequest model);
    Task<ServantWorkDays> GetServantWorkDays(ulong ServantId, ServantWorkDaysRequest model);
    Task<int> GetServantWorkDaysCount(ulong ServantId, ServantWorkDaysRequest model);
}