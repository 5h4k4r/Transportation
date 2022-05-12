
using Core.Requests;
using Infra.Repositories;

namespace Core.Interfaces;

public interface IServantWorkDaysRepository
{
    Task<List<ListServantsOnlineHistory>?> ListServantsOnlineHistory(ListServantsOnlineHistoryRequest model);
    Task<int> ListServantsOnlineHistoryCount(ListServantsOnlineHistoryRequest model);
    Task<ServantOnlinePeriods> GetServantOnlinePeriods(ulong ServantId, GetServantOnlinePeriodsRequest model);
    Task<int> GetServantOnlinePeriodsCount(ulong ServantId, GetServantOnlinePeriodsRequest model);
}