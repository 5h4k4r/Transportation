using Core.Models.Base;
using Core.Models.Common;
using Core.Models.Repositories;
using Core.Models.Requests;

namespace Infra.Interfaces;

public interface ITasksRepository
{
    Task<List<ListTasks>> ListTasks(ListTasksRequest model);
    Task<List<ListTasksByClientResponse>> ListTasksByClient(ulong clientId, ListTasksByClientRequest model);
    Task<int> CountTasks(ListTasksRequest model);
    Task<int> CountClientTasks(ulong clientId, ListTasksByClientRequest model);
    Task<TaskWithDistanceMemberTaxiMeter?> GetActiveTaskByServiceId(ulong userId, uint serviceTypeId);
    Task<RounderDto?> GetLatestRounder(Currency currency);
}