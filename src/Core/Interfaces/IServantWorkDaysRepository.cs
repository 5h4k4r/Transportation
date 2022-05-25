using Core.Models.Repositories;
using Core.Models.Requests;

namespace Core.Interfaces;

public interface IServantWorkDaysRepository
{
    Task<List<ListServantsOnlineHistory>?> ListServantsOnlineHistory(GetServantOnlinePeriodsRequest model);
    Task<int> ListServantsOnlineHistoryCount(GetServantOnlinePeriodsRequest model);
    Task<ServantOnlinePeriods> GetServantOnlinePeriods(ulong servantId, GetServantOnlinePeriodsRequest model);
    Task<int> GetServantOnlinePeriodsCount(ulong servantId, GetServantOnlinePeriodsRequest model);
}