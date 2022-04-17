using Microsoft.EntityFrameworkCore;
using Tranportation.Api.Interfaces;
using Tranportation.Api.Requests;
using Tranportation.Api.Responses;
using Transportation.Api.Extensions;
using Transportation.Api.Interfaces;
using Transportation.Api.Model;

namespace Transportation.Api.Repositories;

public class TasksRepository : ITasksRepository
{
    private readonly transportationContext _context;

    public TasksRepository(transportationContext context)
    {
        _context = context;
    }

    public async Task<List<ListTasksResponse>> ListTasks(ListTasksRequest model)
    {
        var tasks = await GetListTasksQuery(model).ApplySorting(model.SortField, model.SortDescending ?? false).ApplyPagination(model).ToListAsync();

        List<ListTasksResponse> response = tasks.Select(x =>
        {
            var destination = _context.Destinations.Where(x => x.ModelType == @"App\Models\Task").Where(x => x.ModelId == x.Id).FirstOrDefault();


            return new ListTasksResponse
            {
                Id = x.Id,
                RequestId = x.RequestId,
                Price = x.Price,
                Tip = x.Tip,
                Status = x.Status,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Distance = destination.Distance,
                Duration = destination.Duration,
                Servant = new()
                {
                    City = x.Servant.Address,
                    FirstName = x.Servant.FirstName,
                    LastName = x.Servant.LastName,
                    UserId = x.Servant.UserId
                },
                Requester = new()
                {
                    Status = x.Status,
                    Price = x.Price,
                    // Mobile = x.Request.ServiceAreaType.
                },
            };
        }).ToList();

        return response;
    }
    public Task<int> CountTasks(ListTasksRequest model)
    {
        return GetListTasksQuery(model).CountAsync();
    }



    #region Private Functions
    private IQueryable<Model.Task> GetListTasksQuery(ListTasksRequest model)
    {
        var tasksQuery = _context.Tasks.Include(x => x.Request).ThenInclude(x => x.ServiceAreaType).ThenInclude(x => x.Area).Where(x => x.Request.ServiceAreaType.Area.Id == model.AreaId);

        if (model.TaskState != null)
            tasksQuery.Where(x => x.Status == (sbyte)model.TaskState);

        if (model.StartAt != null)
            tasksQuery.Where(x => x.CreatedAt >= model.StartAt.Value.StartOfDay());

        if (model.EndAt != null)
            tasksQuery.Where(x => x.CreatedAt <= model.EndAt.Value.EndOfDay());

        return tasksQuery;
    }


    #endregion
}