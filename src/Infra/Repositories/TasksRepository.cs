using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Constants;
using AutoMapper.QueryableExtensions;
using Core.Constants;
using Core.Extensions;
using Core.Models.Base;
using Core.Models.Common;
using Core.Models.Repositories;
using Core.Models.Requests;
using Infra.Entities;
using Infra.Extensions;
using Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using Task = Infra.Entities.Task;
using ServiceStack;

namespace Infra.Repositories;

public class TasksRepository : ITasksRepository
{
    private readonly TransportationContext _context;
    private readonly IMapper _mapper;

    public TasksRepository(TransportationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ListTasks>> ListTasks(ListTasksRequest model)
    {
        var tasks = await GetListTasksQuery(model)
            .Include(x => x.Servant)
            .Include(x => x.MemberPaymentTypes)
            .ThenInclude(x => x.Member)
            .ThenInclude(x => x.User)
            .Join(
                _context.Destinations,
                task => task.Id,
                destination => destination.ModelId,
                (task, destination) => new
                {
                    Task = task, Destination = destination
                }
            )
            .Where(x => x.Destination.ModelType == "App\\Models\\Task")
            .ApplySorting(model)
            .ApplyPagination(model)
            .AsNoTracking()
            .ToListAsync();

        var response = tasks.Select(x => new
            ListTasks
            {
                Id = x.Task.Id,
                RequestId = x.Task.RequestId,
                Price = x.Task.Price,
                Tip = x.Task.Tip,
                Status = x.Task.Status,
                CreatedAt = x.Task.CreatedAt,
                UpdatedAt = x.Task.UpdatedAt,
                Distance = new TaskDistance
                {
                    Distance = (uint)x.Destination.Distance,
                    Duration = (uint)x.Destination.Duration
                },
                Servant = new ListTasksServant
                {
                    City = x.Task.Servant.Address,
                    FirstName = x.Task.Servant.FirstName,
                    LastName = x.Task.Servant.LastName,
                    UserId = x.Task.Servant.UserId
                },
                Requester = new Requester
                {
                    Id = x.Task.MemberPaymentTypes.FirstOrDefault()?.Member?.Id,
                    Mobile = x.Task.MemberPaymentTypes.FirstOrDefault()?.Member?.User?.Mobile,
                    Name = x.Task.MemberPaymentTypes.FirstOrDefault()?.Member?.User?.Name,
                    Status = x.Task.MemberPaymentTypes.FirstOrDefault()?.Member?.Status
                }
            });

        return response.ToList();
    }
    public async Task<List<ListTasksByClient>> ListTasksByClient(ListTasksByClientRequest model)
    {
        var tasks = await ListTasksByClientRequestQuery(model)
            .ApplySorting(model)
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
        return ListTasksByClientRequestQuery(model).CountAsync();
    }

    public async Task<TaskWithDistanceMemberTaxiMeter?> GetActiveTaskByServiceId(ulong userId, uint serviceTypeId)
    {
        var tasks = await _context.Tasks
            .Where(x => x.Request != null)
            .Where(p => p.Request.ServiceAreaTypeId == serviceTypeId)
            .Join(
                _context.Members.Where(x => x.ModelType == @"App\Models\Task" && x.UserId == userId && x.Requester),
                task => task.Id,
                member => member.ModelId,
                (task, member) => new
                {
                    task, member
                }
            ).Where(p =>
                p.task.Status >= (int)JobStatus.TaskStatus.Accept && p.task.Status < (int)JobStatus.TaskStatus.End)
            .OrderByDescending(p => p.task.CreatedAt)
            .Include(p => p.member.User)
            .Include(p => p.task.Request)
            .ThenInclude(p => p.ServiceAreaType)
            .ThenInclude(p => p.Usage)
            .Include(p => p.member.MemberPaymentTypes)
            .Join(
                _context.TaxiMeters,
                all => all.task.Id,
                meter => meter.TaskId,
                (all, meter) => new
                {
                    all.task, all.member, meter
                })
            .Join(
                _context.DiscountCodeUsers.Where(
                    x => x.ModelType == @"App\Models\Task" && x.UserId == userId && !x.Used),
                all => all.task.Id,
                discountCodeUser => discountCodeUser.ModelId,
                (all, discountCodeUser) => new TaskWithDistanceMemberTaxiMeter
                {
                    Task = _mapper.Map<TaskDto>(all.task),
                    TaxiMeter = _mapper.Map<TaxiMeterDto>(all.meter),
                    Member = _mapper.Map<MemberDto>(all.member),
                    DiscountCodeUser = _mapper.Map<DiscountCodeUserDto>(discountCodeUser),
                    Requester = new Requester()
                    {
                        Id = all.task.MemberPaymentTypes.First().Member.Id,
                        Mobile = all.task.MemberPaymentTypes.First().Member.User.Mobile,
                        Name = all.task.MemberPaymentTypes.First().Member.User.Name,
                        Status = all.task.MemberPaymentTypes.First().Member.Status,
                        PaymentType = all.task.MemberPaymentTypes.First().Type
                    }
                })
            .FirstOrDefaultAsync();
        if (tasks == null) return null;

        var destinationDtos =
            await _context.Destinations
                .Where(x =>
                    x.Status >= (int)JobStatus.DestinationStatus.Active &&
                    x.Status <= (int)JobStatus.DestinationStatus.Arrived &&
                    x.ModelType == @"App\Models\Task" &&
                    x.ModelId == tasks.Task.Id
                )
                .ProjectTo<DestinationDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

        tasks.DestinationDtos = destinationDtos;
        return tasks;
    }

    public Task<RounderDto?> GetLatestRounder(Currency currency)
    {
        return _context.Rounders.Where(x => x.Currency == nameof(currency))
            .ProjectTo<RounderDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
    }

    #region Private Functions

    private IQueryable<Task> GetListTasksQuery(ListTasksRequest model)
    {
        // TODO: unused fields
        var start = model.StartAt.EndOfDay();
        var end = model.EndAt.EndOfDay();

        var query = _context.Tasks
            .Where(x => x.CreatedAt >= start)
            .Where(x => x.CreatedAt <= end)
            .Include(x => x.Request)
            .ThenInclude(x => x.ServiceAreaType)
            .ThenInclude(x => x.Area)
            .Where(x => x.Request.ServiceAreaType.Area.Id == model.AreaId);

        return model.Status.HasValue ? query.Where(x => x.Status == (sbyte)model.Status) : query;
    }

    private IQueryable<ListTasksByClient> ListTasksByClientRequestQuery(ListTasksByClientRequest model)
    {
        var tasksQuery = _context.Tasks;

        var isRequestObjectRequested = model.IncludeRequest.HasValue && model.IncludeRequest.Value;
        var iServantObjectRequested = model.IncludeServant.HasValue && model.IncludeServant.Value;

        if (isRequestObjectRequested)
            tasksQuery.Include(x => x.Request);

        if (model.IncludeServant.HasValue && model.IncludeServant.Value)
            tasksQuery.Include(x => x.Servant);

        var query = tasksQuery.Join(
            _context.Members,
            task => task.Id,
            member => member.ModelId,
            (task, member) => new
            {
                Task = task, Member = member
            }
        ).Where(x => x.Member.ModelType.Contains("Task") && x.Member.UserId == model.ClientId);

        if (model.Status.HasValue)
            query = query.Where(x => x.Task.Status == (sbyte)model.Status);

        if (model.ServantId.HasValue)
            query = query.Where(x => x.Task.ServantId == model.ServantId);

        return query.Select(x =>
            new ListTasksByClient
            {
                Client = _mapper.Map<MemberDto>(x.Member),
                Task = new TaskResponse
                {
                    Id = x.Task.Id,
                    Request = isRequestObjectRequested ? _mapper.Map<RequestDto>(x.Task.Request) : null,
                    Servant = iServantObjectRequested ? _mapper.Map<ServantDto>(x.Task.Servant) : null,
                    Price = x.Task.Price,
                    Tip = x.Task.Tip,
                    Status = x.Task.Status,
                    UpdatedAt = x.Task.UpdatedAt,
                    CreatedAt = x.Task.CreatedAt
                }
            });
    }

    // SELECT * FROM members JOIN tasks WHERE tasks.status = 20 and members.user_id = 47734 and members.model_type like "%Task%" and members.model_id = tasks.id ORDER BY `tasks`.`created_at` ASC

    #endregion
}