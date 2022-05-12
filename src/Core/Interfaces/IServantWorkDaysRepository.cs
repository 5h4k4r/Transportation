
using Core.Requests;
using Infra.Repositories;

namespace Core.Interfaces;

public interface IServantWorkDaysRepository
{
    Task<List<ListServantsOnlineHistory>?> ListServantsOnlineHistory(GetServantOnlinePeriodsRequest model);
    Task<int> ListServantsOnlineHistoryCount(GetServantOnlinePeriodsRequest model);
    Task<ServantOnlinePeriods> GetServantOnlinePeriods(ulong ServantId, GetServantOnlinePeriodsRequest model);
    Task<int> GetServantOnlinePeriodsCount(ulong ServantId, GetServantOnlinePeriodsRequest model);
}