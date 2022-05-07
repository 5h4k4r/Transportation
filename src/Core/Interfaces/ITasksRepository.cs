using Core.Repositories;
using Infra.Requests;
using Infra.Responses;

namespace Core.Interfaces;

public interface ITasksRepository
{
    Task<List<ListTasks>> ListTasks(ListTasksRequest model);
    Task<List<ListTasksByClient>> ListTasksByClient(ListTasksByClientRequest model);
    Task<int> CountTasks(ListTasksRequest model);
    Task<int> CountClientTasks(ListTasksByClientRequest model);

}