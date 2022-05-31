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
using Task = System.Threading.Tasks.Task;
using TaskModel = Infra.Entities.Task;

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

    public Task<ServantDto?> GetServantById(int id, ulong areaId)
    {
        return _context.Servants.Where(x => x.AreaId == areaId).Where(x => x.Id == id)
            .ProjectTo<ServantDto?>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
    }

    public Task<List<ServantDto>> ListServants(ListServantRequest model, ulong userAreaId)
    {
        var query = _context.Servants
            // .Where(x => x.AreaId == userAreaId)
            .ProjectTo<ServantDto>(_mapper.ConfigurationProvider).AsNoTracking();
        if (model.IncompleteOnly)
            return Task.FromResult(
                query
                    .Where(x =>
                        x.FirstName == null ||
                        x.LastName == null ||
                        x.NationalId == null ||
                        x.Certificate == null ||
                        x.BankId == null ||
                        x.GenderId == null ||
                        x.Address == ""
                    )
                    .Join(
                        _context.Documents.Where(x => x.ModelType == "App\\Models\\Servant"),
                        Servant => Servant.Id,
                        Document => (int)Document.ModelId,
                        (Servant, Documents) => new
                        {
                            Servant
                        })
                    .GroupBy(x => x.Servant.Id)
                    .Select(x => x.ToList())
                    .ApplyPagination(model)
                    .ToList()
                    .Where(x => x.Count < 5)
                    .Select(x => x.First().Servant)
                    .ToList()
            );

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
                UpdatedAt = x.UpdatedAt
            })
            .ApplySorting(model)
            .ApplyPagination(model)
            .ToListAsync();
    }

    public Task<int> ListServantsCount(ListServantRequest model, ulong userAreaId)
    {
        var query = _context.Servants
            // .Where(x => x.AreaId == userAreaId)
            .ProjectTo<ServantDto>(_mapper.ConfigurationProvider).AsNoTracking();

        if (model.IncompleteOnly)
            return Task.FromResult(
                query
                    .Where(x =>
                        x.FirstName == null ||
                        x.LastName == null ||
                        x.NationalId == null ||
                        x.Certificate == null ||
                        x.BankId == null ||
                        x.GenderId == null ||
                        x.Address == ""
                    )
                    .Join(
                        _context.Documents.Where(x => x.ModelType == "App\\Models\\Servant"),
                        Servant => Servant.UserId,
                        Document => Document.ModelId,
                        (Servant, Documents) => new
                        {
                            Servant
                        })
                    .GroupBy(x => x.Servant.Id)
                    .OrderBy(x => x.Key)
                    .Select(x => x.ToList())
                    .ToList()
                    .Where(x => x.Count < 5)
                    .Select(x => x.First().Servant)
                    .Count()
            );

        query = CheckForSearchField(query, model);

        return query.CountAsync();
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

    private async Task<(List<TaskModel> Tasks, List<ServantDailyStatistic> DailyStatistics)> FilterTasksAndStatistics(
        ulong servantUserId, ServantPerformanceRequest model)
    {
        var tasksQuery = _context.Tasks.Where(x => x.ServantId == servantUserId);
        var dailyTasksQuery = _context.ServantDailyStatistics.Where(x => x.ServantId == servantUserId);


        List<TaskModel> tasks = new();
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

    private IQueryable<ServantDto> CheckForSearchField(IQueryable<ServantDto> query, ListServantRequest model)
    {
        if (model.SearchField is null || model.SearchValue is null)
            return query;

        return model.SearchField switch
        {
            "Name" => query.Where(
                x => x.FirstName.Contains(model.SearchValue) || x.LastName.Contains(model.SearchValue)),
            "NationalId" => query.Where(x => x.NationalId.Contains(model.SearchValue)),
            "PhoneNumber" => query.Include(x => x.User).Where(x => x.User.Mobile.Contains(model.SearchValue)),
            _ => query.ProjectTo<ServantDto>(_mapper.ConfigurationProvider)
        };
    }
}