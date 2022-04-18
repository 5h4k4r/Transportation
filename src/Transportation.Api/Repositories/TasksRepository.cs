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
        var tasks = await GetListTasksQuery(model).Include(x => x.Servant)
        .Include(x => x.MemberPaymentTypes).ThenInclude(x => x.Member)
        .Join(
            _context.Destinations,
            Task => Task.Id,
            Destination => Destination.ModelId,
            (Task, Destination) => new
            {
                Task,
                Destination
            }
        )
        .ApplySorting(model.SortField, model.SortDescending ?? false)
        .ApplyPagination(model)
        .AsNoTracking()
        .ToListAsync();

        var response = tasks.Select(x => new
       ListTasksResponse
       ()
        {
            Id = x.Task.Id,
            RequestId = x.Task.RequestId,
            Price = x.Task.Price,
            Tip = x.Task.Tip,
            Status = x.Task.Status,
            CreatedAt = x.Task.CreatedAt,
            UpdatedAt = x.Task.UpdatedAt,
            Distance = x.Destination?.Distance,
            Duration = x.Destination?.Duration,
            Servant = new()
            {
                City = x.Task.Servant?.Address,
                FirstName = x.Task.Servant?.FirstName,
                LastName = x.Task.Servant?.LastName,
                UserId = x.Task.Servant?.UserId
            },
            Requester = new()
            {
                Id = x.Task.MemberPaymentTypes.FirstOrDefault()?.Member?.Id,
                Mobile = x.Task.MemberPaymentTypes.FirstOrDefault()?.Member?.User?.Mobile,
                Name = x.Task.MemberPaymentTypes.FirstOrDefault()?.Member?.User?.Name,
                Status = x.Task.MemberPaymentTypes.FirstOrDefault()?.Member?.Status,

            }

        });

        return response.ToList<ListTasksResponse>();
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