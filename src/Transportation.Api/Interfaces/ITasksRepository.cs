using Tranportation.Api.Requests;
using Tranportation.Api.Responses;
using Transportation.Api.Model;

namespace Transportation.Api.Interfaces;

public interface ITasksRepository
{
    Task<List<ListTasksResponse>> ListTasks(ListTasksRequest model);
    Task<int> CountTasks(ListTasksRequest model);

}