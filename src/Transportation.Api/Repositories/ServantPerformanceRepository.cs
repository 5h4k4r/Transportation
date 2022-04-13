using Microsoft.EntityFrameworkCore;
using Transportation.Api.Extensions;
using Transportation.Api.Interfaces;
using Transportation.Api.Model;
using Transportation.Api.Requests;

namespace Transportation.Api.Repositories;

public class ServantPerformanceRepository : IServantsPerformanceRepository
{
    protected transportationContext _context;
    public ServantPerformanceRepository(transportationContext context)
    {
        _context = context;
    }

    public Task<Model.Servant?> GetServantById(int UserId) => _context.Servants.Where(x => x.Id == UserId).FirstOrDefaultAsync();

    public async Task<ServantPerformance?> GetServantPerformance(ServantPerformanceRequest model)
    {
        var (Tasks, DailyStatistics) = await FilterTasksAndStatistics(model);


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
            Tasks = DailyStatistics.Select(x =>
            {
                IEnumerable<Destination> destinations = _context.Destinations.Where(y => y.ModelId == x.Id).ToList();

                Task task = new()
                {
                    Distance = destinations.Sum(d => d.Distance),
                    Duration = destinations.Sum(d => d.Duration),
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                };

                return task;
            }),

        };

        return servantPerformance;


    }

    private async Task<(List<Model.Task> Tasks, List<ServantDailyStatistic> DailyStatistics)> FilterTasksAndStatistics(ServantPerformanceRequest model)
    {
        var tasksQuery = _context.Tasks.Where(x => x.ServantId == model.UserId);
        var dailyTasksQuery = _context.ServantDailyStatistics.Where(x => x.ServantId == model.UserId);


        List<Model.Task> Tasks = new();
        List<Model.ServantDailyStatistic> DailyStatistics = new();

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

}