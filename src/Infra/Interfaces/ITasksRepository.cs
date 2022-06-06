using Core.Models.Common;
using Core.Models.Repositories;
using Core.Models.Requests;

namespace Infra.Interfaces;

public interface ITasksRepository
{
    Task<List<ListTasks>> ListTasks(ListTasksRequest model);
    Task<List<ListTasksByClient>> ListTasksByClient(ListTasksByClientRequest model);
    Task<int> CountTasks(ListTasksRequest model);
    Task<int> CountClientTasks(ListTasksByClientRequest model);
}