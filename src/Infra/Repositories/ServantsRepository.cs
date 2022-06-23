using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Constants;
using Core.Extensions;
using Core.Models.Base;
using Core.Models.Exceptions;
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

    public Task<ServantDto?> GetServantByUserId(ulong userId, ulong areaId)
    {
        return _context.Servants
            .Where(x => x.AreaId == areaId)
            .Where(x => x.UserId == userId)
            .Include(x => x.ServantScores)
            .ProjectTo<ServantDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
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
        var query = _context.Servants.Where(x => x.AreaId == userAreaId)
            .ProjectTo<ServantDto>(_mapper.ConfigurationProvider).AsNoTracking();

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
        var query = _context.Servants.Where(x => x.AreaId == userAreaId)
            .ProjectTo<ServantDto>(_mapper.ConfigurationProvider).AsNoTracking();

        query = CheckForSearchField(query, model);

        return query.CountAsync();
    }

    public async Task<Servant> CreateServant(ServantDto servant)
    {
        var newServant = _mapper.Map<Servant>(servant);
        await _context.Servants.AddAsync(newServant);

        return newServant;
    }

    public async Task<Servant> UpdateServant(UpdateServantRequest model, ulong userId)
    {
        var servant = await _context.Servants.FirstOrDefaultAsync(x => x.UserId == userId);

        if (servant is null)
            throw new NotFoundException("Servant not found");

        servant.Address = model.Address ?? servant.Address;
        servant.Certificate = model.Certificate ?? servant.Certificate;
        servant.GenderId = model.GenderId ?? servant.GenderId;
        servant.FirstName = model.FirstName ?? servant.FirstName;
        servant.LastName = model.LastName ?? servant.LastName;
        servant.NationalId = model.NationalId ?? servant.NationalId;
        servant.UpdatedAt = DateTime.UtcNow;

        if (model.AreaId.HasValue)
            servant.AreaId = model.AreaId.Value;

        var response = _context.Servants.Update(servant).Entity;

        return response;
    }

    public async Task<ServantDto> DeleteServant(ServantDto servant)
    {
        servant.DeletedAt = DateTime.UtcNow;
        var mappedServant = _mapper.Map<Servant>(servant);

        _context.Servants.Update(mappedServant);
        return servant;
    }

    public Task<TaskDto?> ServantActiveTask(ulong id)
    {
        return _context.Tasks
            .Where(x => x.ServantId == id)
            .Where(x => x.Status > (byte)JobStatus.TaskStatus.Accept)
            .ProjectTo<TaskDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Status < (byte)JobStatus.TaskStatus.EndDestination);
    }

    public Task<ServantDto?> GetServantById(int id, ulong areaId)
    {
        return _context.Servants.Where(x => x.AreaId == areaId).Where(x => x.Id == id)
            .ProjectTo<ServantDto?>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
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
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}