using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Constants;
using Core.Extensions;
using Core.Models.Base;
using Core.Models.Exceptions;
using Core.Models.Repositories;
using Core.Models.Requests;
using Core.Models.Responses;
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

    public async Task<ServantPerformance?> GetServantPerformance(GetServantPerformanceRequest model, int servantId,
        ulong servantUserId)
    {
        var (tasks, dailyStatistics) = await FilterTasksAndStatistics(servantUserId, model);
        // await _context.ServantScores.Where(x => x.ServantId == (ulong)servantId).Select(x => x.Score).ToListAsync();

        ServantPerformance servantPerformance = new()
        {
            AcceptedRequests = dailyStatistics.Sum(x => x.DeliveredRequest) -
                               dailyStatistics.Sum(x => x.RejectedRequest),
            DeliveredRequests = dailyStatistics.Sum(x => x.DeliveredRequest),
            RejectedRequests = dailyStatistics.Sum(x => x.RejectedRequest),
            SuccessTasks = tasks.Count(x => x.Status == (sbyte)JobStatus.TaskStatus.End),
            RejectedTasks = dailyStatistics.Sum(x => x.RejectedTask),
            OnlineDurations = dailyStatistics.Sum(x => (int)x.OnlineDuration),
            DurationOnTasks = dailyStatistics.Sum(x => (int)x.DurationOnTask),
            DistanceOnTasks = dailyStatistics.Sum(x => (int)x.DistanceOnTask)
        };


        return servantPerformance;
    }

    public async Task<List<ListServantsPerformances>> ListServantPerformances(ListServantPerformancesRequest model)
    {
        model.StartAt = model.StartAt?.ToUniversalTime();
        model.EndAt = model.EndAt?.ToUniversalTime();
        var servantsQuery = _context.Tasks.AsQueryable();

        if (model.StartAt.HasValue)
            servantsQuery = servantsQuery.Where(x => x.CreatedAt >= model.StartAt.Value);

        if (model.EndAt.HasValue)
            servantsQuery = servantsQuery.Where(x => x.CreatedAt <= model.EndAt.Value);
        
        var response = await servantsQuery.GroupBy(x => x.ServantId).Select(x=> new ListServantsPerformances
        {
            Servant = new ServantPerformed
            {
                Id = (int)x.Key,
                Address = x.First().Servant.Address,
                Certificate = x.First().Servant.Certificate,
                NationalId = x.First().Servant.NationalId,
                UserId = x.First().Servant.UserId,
                FirstName = x.First().Servant.FirstName,
                LastName = x.First().Servant.LastName,
                AreaId = (uint)(x.First().Servant.User.AreaId  ?? 0) ,
                BankId = x.First().Servant.BankId,
                Rating = x.First().Servant.ServantScores.Select(x => x.Score).FirstOrDefault()
            },
            SuccessTasks = x.Count(t => t.Status == (sbyte)JobStatus.TaskStatus.End),
            
        })
            .OrderByDescending(x=>x.SuccessTasks)
            .ApplyPagination(model)
            .ToListAsync();

        return response;
    }

    public Task<List<ServantDto>> ListServants(ListServantRequest model, ulong userAreaId)
    {
        var query = _context.Servants.Where(x => x.AreaId == userAreaId)
            .Include(x=>x.ServantScores)
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
                UpdatedAt = x.UpdatedAt,
                ServantScores = x.ServantScores
            })
            .ApplySorting(model)
            .ApplyPagination(model)
            .ToListAsync();
    }

    public Task<int> ListServantsCount(ListServantRequest model, ulong userAreaId)
    {
        var query = _context.Servants.Where(x => x.AreaId == userAreaId)
            .Include(x=>x.ServantScores)
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

    public async Task<ListServantsWithTheirStatusesResponse> ListServantsWithTheirStatuses(ulong areaId,
        ListServantsWithTheirStatusesRequest model)
    {
        var list = await GetServantsWithTheirStatusesItemsQuery(areaId, model.Status).ToListAsync();
        var offlineCount = await GetServantsWithTheirStatusesItemsQuery(areaId, "offline").CountAsync();
        var onlineCount = await GetServantsWithTheirStatusesItemsQuery(areaId, "online").CountAsync();
        var passiveCount = await GetServantsWithTheirStatusesItemsQuery(areaId, "passive").CountAsync();
        var blockCount = await GetServantsWithTheirStatusesItemsQuery(areaId, "block").CountAsync();
        var inTripCount = await _context.Tasks.Where(x =>
                x.Status >= (sbyte)JobStatus.TaskStatus.Accept && x.Status < (sbyte)JobStatus.TaskStatus.End)
            .CountAsync();

        return new ListServantsWithTheirStatusesResponse
        {
            Offline = offlineCount,
            Online = onlineCount,
            Passive = passiveCount,
            InTrip = inTripCount,
            Block = blockCount,
            Items = list
        };
    }

    public Task<ServiceResponse?> GetServantsServices(ulong id, ulong langId)
    {
        return _context.Servants.Where(v => v.UserId == id)
            .Join(_context.ServiceSubscribers,
                servant => servant.UserId,
                serviceSubscriber => serviceSubscriber.ModelId,
                (vehicle, service) => new
                {
                    vehicle,
                    service.ServiceAreaTypeId
                })
            .Join(_context.ServiceAreaTypes, v => v.ServiceAreaTypeId, sat => sat.Id, (vs, serviceAreaType) => new
            {
                vs.vehicle,
                serviceAreaTypeId = serviceAreaType.Id,
                service = serviceAreaType.Service.Pin, // Taxi
                areaId = serviceAreaType.AreaId,
                typeId = serviceAreaType.TypeId
            })
            .Join(_context.BaseTypeTranslations.Where(x => x.LanguageId == langId),
                x => x.typeId,
                bt => bt.BaseTypeId,
                (vs, bt) => new
                {
                    vs,
                    bt.Title // gunjaw w xera
                }).Join(_context.AreaInfos, vs => vs.vs.areaId, aInfo => aInfo.AreaId, (vsa, area) => new
            {
                vsa,
                area.Title //Sulaimani
            })
            .Select(x => new ServiceResponse
                {
                    Id = x.vsa.vs.serviceAreaTypeId,
                    Title = $"{x.vsa.vs.service} {x.vsa.Title} {x.Title}"
                }
            ).FirstOrDefaultAsync();
    }

    public Task<ServantDto?> GetServantById(int id, ulong areaId)
    {
        return _context.Servants.Where(x => x.AreaId == areaId).Where(x => x.Id == id)
            .ProjectTo<ServantDto?>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
    }


    private async Task<(List<Task> Tasks, List<ServantDailyStatistic> DailyStatistics)> FilterTasksAndStatistics(
        ulong servantUserId, GetServantPerformanceRequest model)
    {
        model.StartAt = model.StartAt?.ToUniversalTime();
        model.EndAt = model.EndAt?.ToUniversalTime();

        var tasksQuery = _context.Tasks.Where(x => x.ServantId == servantUserId);
        var dailyTasksQuery = _context.ServantDailyStatistics.Where(x => x.ServantId == servantUserId);


        List<Task> tasks = new();
        List<ServantDailyStatistic> dailyStatistics = new();

        var today = DateTime.UtcNow;

        if (model.StartAt is null)
        {
            tasksQuery = tasksQuery
                .Where(x => x.CreatedAt <= today.StartOfDay())
                .Where(x => x.CreatedAt >= today.EndOfDay());

            dailyTasksQuery = dailyTasksQuery
                .Where(x => x.Day != null)
                .Where(x => x.Day!.Date == DateOnly.FromDateTime(today));
        }
        else if (model.StartAt != null && model.EndAt == null)
        {
            tasksQuery = tasksQuery
                .Where(x => x.CreatedAt >= model.StartAt)
                .Where(x => x.CreatedAt <= today.EndOfDay());

            dailyTasksQuery = dailyTasksQuery
                    .Where(x => x.Day != null)
                    .Where(x => x.Day!.Date >= DateOnly.FromDateTime(model.StartAt ?? today.StartOfDay()))
                    .Where(x => x.Day!.Date <= DateOnly.FromDateTime(today.EndOfDay()));
        }
        else if (model.StartAt != null && model.EndAt != null)
        {
            tasksQuery = tasksQuery
                .Where(x => x.CreatedAt >= model.StartAt)
                .Where(x => x.CreatedAt <= model.EndAt);

            var startDate = DateOnly.FromDateTime(model.StartAt ?? today.StartOfDay());
            var endDate = DateOnly.FromDateTime(model.EndAt ?? today.EndOfDay());

            dailyTasksQuery = dailyTasksQuery
                .OrderBy(x => x.DayId)
                .Where(x => x.Day != null)
                .Where(x => x.Day!.Date >= startDate)
                .Where(x => x.Day!.Date <= endDate);


        }

        dailyStatistics = await dailyTasksQuery.ToListAsync();
        tasks = await tasksQuery.ToListAsync();
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

    private IQueryable<ListServantsWithTheirStatusesItem> GetServantsWithTheirStatusesItemsQuery(ulong areaId,
        string status)
    {
        return _context.ServantStatuses
            .Where(x => x.Status.Equals(status))
            .Include(x => x.Service)
            .Include(x => x.Servant)
            .ThenInclude(x => x.User)
            .OrderByDescending(x => x.UpdatedAt)
            .Where(x => x.Servant.AreaId == areaId)
            .Select(x => new ListServantsWithTheirStatusesItem
            {
                Location = new ListServantsWithTheirStatusesLocation
                {
                    Lat = x.Lat,
                    Lng = x.Lng,
                    Bearing = 0
                },
                User = new ListServantsWithTheirStatusesUser
                {
                    Id = x.Servant.User.Id,
                    FirstName = x.Servant.FirstName,
                    LastName = x.Servant.LastName,
                    Mobile = x.Servant.User.Mobile
                },
                Service = new ListServantsWithTheirStatusesService
                {
                    Id = x.Service.Id,
                    Pin = x.Service.Pin
                }
            });
    }
}