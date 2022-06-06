using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Extensions;
using Core.Models.Base;
using Core.Models.Repositories;
using Core.Models.Requests;
using Infra.Entities;
using Infra.Extensions;
using Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using Task = Infra.Entities.Task;

namespace Infra.Repositories;

public class ServantsRepository : IServantsRepository
{
    private readonly TransportationContext _context;
    private readonly IMapper _mapper;

    public ServantsRepository(TransportationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<ServantPerformance?> GetServantPerformance(ServantPerformanceRequest model, int servantId,
        ulong servantUserId)
    {
        var (_, dailyStatistics) = await FilterTasksAndStatistics(servantUserId, model);
        await _context.ServantScores.Where(x => x.ServantId == (ulong)servantId).Select(x => x.Score).ToListAsync();

        ServantPerformance servantPerformance = new()
        {
            AcceptedRequests = dailyStatistics.Sum(x => x.DeliveredRequest) -
                               dailyStatistics.Sum(x => x.RejectedRequest),
            DeliveredRequests = dailyStatistics.Sum(x => x.DeliveredRequest),
            RejectedRequests = dailyStatistics.Sum(x => x.RejectedRequest),
            SuccessTasks = dailyStatistics.Sum(x => x.SuccessTask),
            RejectedTasks = dailyStatistics.Sum(x => x.RejectedTask),
            OnlineDurations = dailyStatistics.Sum(x => (int)x.OnlineDuration),
            DurationOnTasks = dailyStatistics.Sum(x => (int)x.DurationOnTask),
            DistanceOnTasks = dailyStatistics.Sum(x => (int)x.DistanceOnTask)
        };


        return servantPerformance;
    }

    public Task<List<ServantDto>> ListServants(ListServantRequest model, ulong userAreaId)
    {
        var query = _context.Servants
            .Where(x => x.AreaId == userAreaId);

        query = CheckForSearchField(query, model);

        if (model.IncompleteOnly)
            query = query
                .Where(x =>
                    (x.FirstName == null ||
                     x.LastName == null ||
                     x.NationalId == null ||
                     x.Certificate == null ||
                     x.BankId == null ||
                     x.GenderId == null ||
                     x.Address == null ||
                     _context.Documents
                         .Where(d => d.ModelId == x.UserId)
                         .Count(d => d.ModelType == @"App\Models\Servant") < 5
                     ||
                     !_context.Documents
                         .Where(d => d.ModelType == @"App\Models\Servant")
                         .Any(d => d.ModelId == x.UserId)
                    ) && x.DeletedAt == null
                );


        return query
            .ProjectTo<ServantDto>(_mapper.ConfigurationProvider)
            .Select(x => new ServantDto
            {
                Id = x.Id,
                UserId = x.UserId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                NationalId = x.NationalId,
                Certificate = x.Certificate,
                BankId = x.BankId,
                AreaId = x.AreaId,
                GenderId = x.GenderId,
                Address = x.Address,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            })
            .AsNoTracking()
            .ApplySorting(model)
            .ApplyPagination(model).ToListAsync();
    }

    public Task<int> ListServantsCount(ListServantRequest model, ulong userAreaId)
    {
        var query = _context.Servants
            .Where(x => x.AreaId == userAreaId);

        query = CheckForSearchField(query, model);

        if (model.IncompleteOnly)
            query = query
                .Where(x =>
                    (x.FirstName == null ||
                     x.LastName == null ||
                     x.NationalId == null ||
                     x.Certificate == null ||
                     x.BankId == null ||
                     x.GenderId == null ||
                     x.Address == null ||
                     _context.Documents
                         .Where(d => d.ModelId == x.UserId)
                         .Count(d => d.ModelType == @"App\Models\Servant") < 5
                     ||
                     !_context.Documents
                         .Where(d => d.ModelType == @"App\Models\Servant")
                         .Any(d => d.ModelId == x.UserId)
                    ) && x.DeletedAt == null
                );

        return query
            .ProjectTo<ServantDto>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .CountAsync();
    }

    public async void CreateServant(ServantDto servant)
    {
        var newServant = _mapper.Map<Servant>(servant);
        await _context.Servants.AddAsync(newServant);
    }

    public Task<ServantDto?> GetServantByUserId(ulong userId, ulong areaId)
    {
        return _context.Servants
            .Where(x => x.AreaId == areaId)
            .Where(x => x.UserId == userId)
            .Include(x => x.ServantScores)
            .ProjectTo<ServantDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }

    private async Task<(List<Task> Tasks, List<ServantDailyStatistic> DailyStatistics)> FilterTasksAndStatistics(
        ulong servantUserId, ServantPerformanceRequest model)
    {
        var tasksQuery = _context.Tasks.Where(x => x.ServantId == servantUserId);
        var dailyTasksQuery = _context.ServantDailyStatistics.Where(x => x.ServantId == servantUserId);


        List<Task> tasks = new();
        List<ServantDailyStatistic> dailyStatistics = new();

        var today = DateTime.UtcNow;

        if (model.StartAt is null)
        {
            tasks = await tasksQuery
                .Where(x => x.CreatedAt <= today.StartOfDay())
                .Where(x => x.CreatedAt >= today.EndOfDay())
                .ToListAsync();

            dailyStatistics = await dailyTasksQuery
                .Where(x => x.Day != null)
                .Where(x => x.Day!.Date == DateOnly.FromDateTime(today))
                .ToListAsync();
        }
        else if (model.StartAt != null && model.EndAt == null)
        {
            tasks = await tasksQuery
                .Where(x => x.CreatedAt >= model.StartAt)
                .Where(x => x.CreatedAt <= today.EndOfDay())
                .ToListAsync();

            dailyStatistics =
                await dailyTasksQuery
                    .Where(x => x.Day != null)
                    .Where(x => x.Day!.Date >= DateOnly.FromDateTime(model.StartAt ?? today.StartOfDay()))
                    .Where(x => x.Day!.Date <= DateOnly.FromDateTime(today.EndOfDay()))
                    .ToListAsync()
                ;
        }
        else if (model.StartAt != null && model.EndAt != null)
        {
            tasks = await tasksQuery.Where(x => x.CreatedAt >= model.StartAt).Where(x => x.CreatedAt <= model.EndAt)
                .ToListAsync();

            var startDate = DateOnly.FromDateTime(model.StartAt ?? today.StartOfDay());
            var endDate = DateOnly.FromDateTime(model.EndAt ?? today.EndOfDay());

            dailyStatistics = await dailyTasksQuery
                .OrderBy(x => x.DayId)
                .Where(x => x.Day != null)
                .Where(x => x.Day!.Date >= startDate)
                .Where(x => x.Day!.Date <= endDate)
                .ToListAsync();
        }


        return (tasks, dailyStatistics);
    }

    private IQueryable<Servant> CheckForSearchField(IQueryable<Servant> query, ListServantRequest model)
    {
        if (model.SearchField is null || model.SearchValue is null)
            return query;

        return model.SearchField switch
        {
            "Name" => query.Where(
                x => x.FirstName.Contains(model.SearchValue) || x.LastName.Contains(model.SearchValue)),
            "NationalId" => query.Where(x => x.NationalId.Contains(model.SearchValue)),
            "PhoneNumber" => query.Include(x => x.User).Where(x => x.User.Mobile.Contains(model.SearchValue))
        };
    }
}