using AutoMapper;
using Core.Extensions;
using Core.Interfaces;
using Core.Models;
using Core.Repositories;
using Infra.Entities;
using Infra.Extensions;
using Infra.Requests;
using Infra.Responses;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class TasksRepository : ITasksRepository
{
    private readonly transportationContext _context;
    private readonly IMapper _mapper;

    public TasksRepository(transportationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ListTasks>> ListTasks(ListTasksRequest model)
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
        .Where(x => x.Destination.ModelType == "App\\Models\\Task")
        .ApplySorting(model.SortField, model.SortDescending ?? false)
        .ApplyPagination(model)
        .AsNoTracking()
        .ToListAsync();

        var response = tasks.Select(x => new
       ListTasks
       ()
        {
            Id = x.Task.Id,
            RequestId = x.Task.RequestId,
            Price = x.Task.Price,
            Tip = x.Task.Tip,
            Status = x.Task.Status,
            CreatedAt = x.Task.CreatedAt,
            UpdatedAt = x.Task.UpdatedAt,
            Distance = new()
            {
                Distance = x.Destination?.Distance,
                Duration = x.Destination?.Duration,
            },
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

        return response.ToList();
    }
    public async Task<List<ListTasksByClient>> ListTasksByClient(ListTasksByClientRequest model)
    {
        var tasks = await GetListTasksByClientRequestQuery(model)
        .ApplySorting(model.SortField, model.SortDescending ?? false)
        .ApplyPagination(model)
        .AsNoTracking()
        .ToListAsync();



        return tasks;
    }
    public Task<int> CountTasks(ListTasksRequest model)
    {
        return GetListTasksQuery(model).CountAsync();
    }
    public Task<int> CountClientTasks(ListTasksByClientRequest model)
    {
        return GetListTasksByClientRequestQuery(model).CountAsync();
    }



    #region Private Functions
    private IQueryable<Entities.Task> GetListTasksQuery(ListTasksRequest model)
    {
        var start = model.StartAt.StartOfDay();
        var end = model.EndAt.EndOfDay();

        var query = _context.Tasks
           .Where(x => x.CreatedAt >= model.StartAt.EndOfDay())
           .Where(x => x.CreatedAt <= model.EndAt.EndOfDay())
           .Include(x => x.Request)
           .ThenInclude(x => x.ServiceAreaType)
           .ThenInclude(x => x.Area)
           .Where(x => x.Request.ServiceAreaType.Area.Id == model.AreaId);

        if (model.Status.HasValue)
            return query.Where(x => x.Status == (sbyte)model.Status);

        return query;
    }

    private IQueryable<ListTasksByClient> GetListTasksByClientRequestQuery(ListTasksByClientRequest model)
    {
        var tasksQuery = _context.Tasks;

        bool isRequestObjectRequested = model.IncludeRequest.HasValue && model.IncludeRequest.Value;
        bool iServantObjectRequested = model.IncludeServant.HasValue && model.IncludeServant.Value;

        if (isRequestObjectRequested)
            tasksQuery.Include(x => x.Request);

        if (model.IncludeServant.HasValue && model.IncludeServant.Value)
            tasksQuery.Include(x => x.Servant);

        var query = tasksQuery.Join(
            _context.Members,
            Task => Task.Id,
            Member => Member.ModelId,
            (Task, Member) => new
            {
                Task,
                Member
            }
        ).Where(x => x.Member.ModelType.Contains("Task") && x.Member.UserId == model.ClientId);

        if (model.Status.HasValue)
            query = query.Where(x => x.Task.Status == (sbyte)model.Status);

        if (model.ServantId.HasValue)
            query = query.Where(x => x.Task.ServantId == model.ServantId);

        return query.Select(x =>
             new ListTasksByClient
             {
                 Client = _mapper.Map<MemberDTO>(x.Member),
                 Task = new TaskResponse
                 {
                     Id = x.Task.Id,
                     Request = isRequestObjectRequested ? _mapper.Map<RequestDTO>(x.Task.Request) : null,
                     Servant = iServantObjectRequested ? _mapper.Map<ServantDTO>(x.Task.Servant) : null,
                     Price = x.Task.Price,
                     Tip = x.Task.Tip,
                     Status = x.Task.Status,
                     UpdatedAt = x.Task.UpdatedAt,
                     CreatedAt = x.Task.CreatedAt,
                 }
             });

    }

    // SELECT * FROM members JOIN tasks WHERE tasks.status = 20 and members.user_id = 47734 and members.model_type like "%Task%" and members.model_id = tasks.id ORDER BY `tasks`.`created_at` ASC

    #endregion
}