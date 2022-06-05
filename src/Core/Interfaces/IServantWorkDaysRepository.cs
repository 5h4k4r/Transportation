using Core.Models.Repositories;
using Core.Models.Requests;

namespace Core.Interfaces;

public interface IServantWorkDaysRepository
{
    Task<List<ListServantsOnlineHistory>?> ListServantsOnlineHistory(ListServantsOnlineHistoryRequest model);
    Task<List<ServantOnlinePeriod>> GetServantOnlineHistory(ulong servantId, GetServantOnlineHistoryRequest model);
    Task<int> GetServantOnlineHistoryCount(ulong servantId, GetServantOnlineHistoryRequest model);
}