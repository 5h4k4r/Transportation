using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Extensions;
using Core.Interfaces;
using Core.Models.Base;
using Core.Models.Repositories;
using Core.Models.Requests;
using Infra.Entities;
using Infra.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class ServantsRepository : IServantsRepository
{
    protected TransportationContext _Context;
    private readonly IMapper _mapper;
    public ServantsRepository(TransportationContext context, IMapper mapper)
    {
        _Context = context;
        _mapper = mapper;
    }

    public Task<ServantDto?> GetServantById(ulong userId, ulong areaId) => _Context.Servants
    .Where(x => x.AreaId == areaId)
    .Where(x => x.UserId == userId)
    .Include(x => x.ServantScores)
    .ProjectTo<ServantDto>(_mapper.ConfigurationProvider)
    .FirstOrDefaultAsync();


    public async Task<ServantPerformance?> GetServantPerformance(ServantPerformanceRequest model, int servantId, ulong servantUserId)
    {

        var (_, dailyStatistics) = await FilterTasksAndStatistics(servantUserId, model);
        await _Context.ServantScores.Where(x => x.ServantId == (ulong)servantId).Select(x => x.Score).ToListAsync();

        ServantPerformance servantPerformance = new()
        {
            AcceptedRequests = dailyStatistics.Sum(x => x.DeliveredRequest) - dailyStatistics.Sum(x => x.RejectedRequest),
            DeliveredRequests = dailyStatistics.Sum(x => x.DeliveredRequest),
            RejectedRequests = dailyStatistics.Sum(x => x.RejectedRequest),
            SuccessTasks = dailyStatistics.Sum(x => x.SuccessTask),
            RejectedTasks = dailyStatistics.Sum(x => x.RejectedTask),
            OnlineDurations = dailyStatistics.Sum(x => (int)x.OnlineDuration),
            DurationOnTasks = dailyStatistics.Sum(x => (int)x.DurationOnTask),
            DistanceOnTasks = dailyStatistics.Sum(x => (int)x.DistanceOnTask),
        };


        return servantPerformance;


    }

    private async Task<(List<Entities.Task> Tasks, List<ServantDailyStatistic> DailyStatistics)> FilterTasksAndStatistics(ulong servantUserId, ServantPerformanceRequest model)
    {
        var tasksQuery = _Context.Tasks.Where(x => x.ServantId == servantUserId);
        var dailyTasksQuery = _Context.ServantDailyStatistics.Where(x => x.ServantId == servantUserId);


        List<Entities.Task> tasks = new();
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
            tasks = await tasksQuery.Where(x => x.CreatedAt >= model.StartAt).Where(x => x.CreatedAt <= model.EndAt).ToListAsync();

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

    public Task<ServantDto?> GetServantById(int id, ulong userAreaId)
    {
        return _Context.Servants.Include(x => x.AreaId == userAreaId).Where(x => x.Id == id).ProjectTo<ServantDto?>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
    }

    public Task<List<ServantDto>> ListServants(ListServantRequest model, ulong userAreaId)
    {
        var query = _Context.Servants.Where(x => x.AreaId == userAreaId);
        if (model.IncompleteOnly)
        {
            return query
            .Where(x =>
            x.Certificate == null ||
            x.BankId == null ||
            x.GenderId == null ||
            x.Address == null
            )
            .Join(
                _Context.Documents.Where(x => x.ModelType == "App\\Models\\Servant"),
                servants => (ulong)servants.Id,
                documents => documents.ModelId,
                (servants, documents) => new { Servants = servants, Documents = documents }
            )
            .Select(x => x.Servants)
            .ProjectTo<ServantDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        }

        if (model.SearchField is null || model.SearchValue is null)
            return query.ProjectTo<ServantDto>(_mapper.ConfigurationProvider).ToListAsync();


        if (model.SearchField == "Name")
            query = query.Where(x => x.FirstName.Contains(model.SearchValue) || x.LastName.Contains(model.SearchValue));

        else if (model.SearchField == "NationalId")
            query = query.Where(x => x.NationalId.Contains(model.SearchValue));


        else if (model.SearchField == "PhoneNumber")
            query = query.Include(x => x.User).Where(x => x.User.Mobile.Contains(model.SearchValue));


        return query.ProjectTo<ServantDto>(_mapper.ConfigurationProvider).ApplyPagination(model).ToListAsync(); 

    }

    public async void CreateServant(ServantDto servant)
    {
        var newServant = _mapper.Map<Servant>(servant);
        await _Context.Servants.AddAsync(newServant);
    }

}