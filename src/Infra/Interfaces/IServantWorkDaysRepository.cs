using Core.Models.Repositories;
using Core.Models.Requests;
using Core.Models.Responses;

namespace Infra.Interfaces;

public interface IServantWorkDaysRepository
{
    Task<List<ListServantsOnlineHistory>?> ListServantsOnlineHistory(ListServantsOnlineHistoryRequest model);

    Task<List<ServantOnlinePeriod>> GetServantOnlinePeriods(ulong servantId, GetServantOnlineHistoryRequest model);
    Task<int> GetServantOnlinePeriodsCount(ulong servantId, GetServantOnlineHistoryRequest model);

    Task<ListServantsOnlineHistory> GetServantOnlinePeriodsTotalTime(GetServantOnlineHistoryRequest model,
        ulong? servantId = null);
}