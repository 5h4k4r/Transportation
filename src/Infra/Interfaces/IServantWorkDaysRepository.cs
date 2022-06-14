using Core.Models.Repositories;
using Core.Models.Requests;
using Core.Models.Responses;

namespace Infra.Interfaces;

public interface IServantWorkDaysRepository
{
    Task<List<ListServantsOnlineHistory>?> ListServantsOnlineHistory(ListServantsOnlineHistoryRequest model,
        ulong? servantId = null);

    Task<List<ServantOnlinePeriod>> GetServantOnlineHistory(ulong servantId, GetServantOnlineHistoryRequest model);
    Task<int> GetServantOnlineHistoryCount(ulong servantId, GetServantOnlineHistoryRequest model);
}