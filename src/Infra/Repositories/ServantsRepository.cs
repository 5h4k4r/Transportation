using System.Reflection.Metadata;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Extensions;
using Core.Interfaces;
using Core.Models;
using Core.Models.Base;
using Core.Models.Repositories;
using Core.Models.Requests;
using Infra.Entities;
using Infra.Extensions;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Infra.Repositories;

public class ServantsRepository : IServantsRepository
{
    protected TransportationContext _context;
    private readonly IMapper _mapper;
    public ServantsRepository(TransportationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public Task<ServantDto?> GetServantByUserId(ulong UserId, ulong AreaId) => _context.Servants
    .Where(x => x.AreaId == AreaId)
    .Where(x => x.UserId == (ulong)UserId)
    .Include(x => x.ServantScores)
    .ProjectTo<ServantDto>(_mapper.ConfigurationProvider)
    .FirstOrDefaultAsync();


    public async Task<ServantPerformance?> GetServantPerformance(ServantPerformanceRequest model, int ServantId, ulong ServantUserId)
    {

        var (Tasks, DailyStatistics) = await FilterTasksAndStatistics(ServantUserId, model);
        IEnumerable<double>? Rates = await _context.ServantScores.Where(x => x.ServantId == (ulong)ServantId).Select(x => x.Score).ToListAsync();

        ServantPerformance servantPerformance = new()
        {
            AcceptedRequests = DailyStatistics.Sum(x => x.DeliveredRequest) - DailyStatistics.Sum(x => x.RejectedRequest),
            DeliveredRequests = DailyStatistics.Sum(x => x.DeliveredRequest),
            RejectedRequests = DailyStatistics.Sum(x => x.RejectedRequest),
            SuccessTasks = DailyStatistics.Sum(x => x.SuccessTask),
            RejectedTasks = DailyStatistics.Sum(x => x.RejectedTask),
            OnlineDurations = DailyStatistics.Sum(x => (int)x.OnlineDuration),
            DurationOnTasks = DailyStatistics.Sum(x => (int)x.DurationOnTask),
            DistanceOnTasks = DailyStatistics.Sum(x => (int)x.DistanceOnTask),
        };


        return servantPerformance;


    }

    private async Task<(List<Entities.Task> Tasks, List<ServantDailyStatistic> DailyStatistics)> FilterTasksAndStatistics(ulong ServantUserId, ServantPerformanceRequest model)
    {
        var tasksQuery = _context.Tasks.Where(x => x.ServantId == ServantUserId);
        var dailyTasksQuery = _context.ServantDailyStatistics.Where(x => x.ServantId == ServantUserId);


        List<Entities.Task> Tasks = new();
        List<ServantDailyStatistic> DailyStatistics = new();

        var today = DateTime.UtcNow;

        if (model.StartAt is null)
        {
            Tasks = await tasksQuery
            .Where(x => x.CreatedAt <= today.StartOfDay())
            .Where(x => x.CreatedAt >= today.EndOfDay())
            .ToListAsync();

            DailyStatistics = await dailyTasksQuery
            .Where(x => x.Day != null)
            .Where(x => x.Day.Date == DateOnly.FromDateTime(today))
            .ToListAsync();

        }
        else if (model.StartAt != null && model.EndAt == null)
        {
            Tasks = await tasksQuery
            .Where(x => x.CreatedAt >= model.StartAt)
            .Where(x => x.CreatedAt <= today.EndOfDay())
            .ToListAsync();

            DailyStatistics =
                await dailyTasksQuery
                .Where(x => x.Day != null)
                .Where(x => x.Day.Date >= DateOnly.FromDateTime(model.StartAt ?? today.StartOfDay()))
                .Where(x => x.Day.Date <= DateOnly.FromDateTime(today.EndOfDay()))
                .ToListAsync()
                ;

        }
        else if (model.StartAt != null && model.EndAt != null)
        {
            Tasks = await tasksQuery.Where(x => x.CreatedAt >= model.StartAt).Where(x => x.CreatedAt <= model.EndAt).ToListAsync();

            var startDate = DateOnly.FromDateTime(model.StartAt ?? today.StartOfDay());
            var endDate = DateOnly.FromDateTime(model.EndAt ?? today.EndOfDay());

            DailyStatistics = await dailyTasksQuery
            .OrderBy(x => x.DayId)
            .Where(x => x.Day != null)
            .Where(x => x.Day.Date >= startDate)
            .Where(x => x.Day.Date <= endDate)
            .ToListAsync();
        }


        return (Tasks, DailyStatistics);


    }
    public Task<ServantDto?> GetServantById(int Id, ulong areaId)
    {
        return _context.Servants.Where(x => x.AreaId == areaId).Where(x => x.Id == Id).ProjectTo<ServantDto?>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
    }
    public Task<List<ServantDto>> ListServants(ListServantRequest model, ulong UserAreaId)
    {
        var query = _context.Servants.Where(x => x.AreaId == UserAreaId).ProjectTo<ServantDto>(_mapper.ConfigurationProvider).AsNoTracking();

        query = CheckForSearchField(query, model);


        return query
                    .Select(x => new ServantDto
                    {
                        Address = x.Address,
                        AreaId = x.AreaId,
                        CreatedAt = x.CreatedAt,
                        Id = x.Id,
                        UserId = x.UserId,
                        BankId = x.BankId,
                        Certificate = x.Certificate,
                        NationalId = x.NationalId,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        GenderId = x.GenderId,
                        UpdatedAt = x.UpdatedAt,
                    })
                    .ApplySorting(model)
                    .ApplyPagination(model)
                    .ToListAsync();

    }
    public Task<int> ListServantsCount(ListServantRequest model, ulong UserAreaId)
    {
        var query = _context.Servants.Where(x => x.AreaId == UserAreaId).ProjectTo<ServantDto>(_mapper.ConfigurationProvider).AsNoTracking();

        query = CheckForSearchField(query, model);

        return query.CountAsync();

    }

    private static IQueryable<ServantDto> CheckForSearchField(IQueryable<ServantDto> query, ListServantRequest model)
    {


        if (model.SearchField is null || model.SearchValue is null)
            return query;

        if (model.SearchField == "Name")
            return query.Where(x => x.FirstName.Contains(model.SearchValue) || x.LastName.Contains(model.SearchValue));

        else if (model.SearchField == "NationalId")
            return query.Where(x => x.NationalId.Contains(model.SearchValue));


        else if (model.SearchField == "PhoneNumber")
            return query.Include(x => x.User).Where(x => x.User.Mobile.Contains(model.SearchValue));

        return query;

    }

    public async void CreateServant(ServantDto servant)
    {
        var newServant = _mapper.Map<Servant>(servant);
        await _context.Servants.AddAsync(newServant);
    }
}