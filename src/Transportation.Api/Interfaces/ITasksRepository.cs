using Tranportation.Api.Requests;
using Tranportation.Api.Responses;
using Transportation.Api.Model;
using Transportation.Api.Responses;

namespace Transportation.Api.Interfaces;

public interface ITasksRepository
{
    Task<List<ListTasksResponse>> ListTasks(ListTasksRequest model);
    Task<List<ListTasksByClientResponse>> ListTasksByClient(ListTasksByClientRequest model);
    Task<int> CountTasks(ListTasksRequest model);
    Task<int> CountClientTasks(ListTasksByClientRequest model);

}