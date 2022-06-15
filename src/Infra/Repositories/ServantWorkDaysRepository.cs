using Core.Helpers;
using Core.Models.Repositories;
using Core.Models.Requests;
using Core.Models.Responses;
using Infra.Entities;
using Infra.Extensions;
using Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class ServantWorkDaysRepository : IServantWorkDaysRepository
{
    private readonly TransportationContext _context;


    public ServantWorkDaysRepository(TransportationContext repositoryContext)
    {
        _context = repositoryContext;
    }

    public async Task<List<ServantOnlinePeriod>> GetServantOnlinePeriods(ulong servantId,
        GetServantOnlineHistoryRequest model)
    {
        var excludeStartHour = model.ExcludeStartHour;
        var excludeEndHour = model.ExcludeEndHour;

        var dailyStats = await GetServantOnlinePeriodsQuery(model, servantId)
            .OrderByDescending(x => x.StartAt)
            .GroupBy(x => x.ServantDailyStatistic.DayId)
            .Select(x => x.ToList())
            .ApplyPagination(model).ToListAsync();

        var servantWorkDayPeriods = dailyStats.Select(workDay =>
        {
            var hours = workDay.Select(workingPeriod =>
                {
                    var startAt = workingPeriod.StartAt;
                    var endAt = workingPeriod.EndAt;


                    var startOfExcludedTime =
                        new DateTime(startAt.Year, startAt.Month, startAt.Day, excludeStartHour ?? 0, 0, 0, 0);

                    var endOfExcludedTime =
                        new DateTime(startAt.Year, startAt.Month, startAt.Day, excludeEndHour ?? 0, 0, 0, 0);


                    var totalDiffInSeconds = DateIntervalHelpers.CalculateHoursExcludingRange(startAt, endAt,
                        startOfExcludedTime, endOfExcludedTime) * 3600;

                    var diffInTime = TimeSpan.FromSeconds(totalDiffInSeconds);

                    return new ServantOnlinePeriodItem
                    {
                        StartAt = startAt,
                        EndAt = endAt,
                        DiffInTime = diffInTime,
                        TotalPeriodInSeconds = totalDiffInSeconds
                    };
                })
                //dont return period when diff is 0 or less
                .Where(x => x.TotalPeriodInSeconds > 0).OrderBy(x => x.StartAt).ToList();

            var totalTimeInSeconds = hours.Sum(x => x.TotalPeriodInSeconds);
            var diffInTime = TimeSpan.FromSeconds(totalTimeInSeconds);


            return new ServantOnlinePeriod
            {
                TotalOnlineTime = diffInTime,
                Date = workDay.FirstOrDefault()?.StartAt.Date,
                Periods = hours
            };
            // dont return the day when its periods are empty
        }).Where(x => x.Periods.Any()).ToList();

        return servantWorkDayPeriods;
    }

    public Task<int> GetServantOnlinePeriodsCount(ulong servantId, GetServantOnlineHistoryRequest model)
    {
        var query = GetServantOnlinePeriodsQuery(model, servantId);

        var count = query
            .OrderByDescending(x => x.StartAt)
            .GroupBy(x => x.ServantDailyStatistic.DayId)
            .CountAsync();


        return count;
    }

    public async Task<ListServantsOnlineHistory> GetServantOnlinePeriodsTotalTime(
        GetServantOnlineHistoryRequest model, ulong? servantId = null)
    {
        var excludeStartHour = model.ExcludeStartHour ?? null;
        var excludeEndHour = model.ExcludeEndHour ?? null;
        IQueryable<ServantDailyOnlinePeriod> query;

        if (servantId.HasValue)
            query = GetServantOnlinePeriodsQuery(model, servantId);
        else
            query = GetServantOnlinePeriodsQuery(model);

        var onlineHistory = await query
            .OrderByDescending(x => x.StartAt)
            .GroupBy(x => x.ServantDailyStatistic.ServantId)
            .Select(x => x.ToList())
            .ToListAsync();

        var response = onlineHistory.Select(day =>
        {
            var onlineSecondsOfDriver = day.Select(x =>
            {
                var startAt = x.StartAt;
                var endAt = x.EndAt;

                var startOfExcludedTime =
                    new DateTime(startAt.Year, startAt.Month, startAt.Day, excludeStartHour ?? 0, 0, 0, 0);
                var endOfExcludedTime =
                    new DateTime(startAt.Year, startAt.Month, startAt.Day, excludeEndHour ?? 0, 0, 0, 0);


                var totalDiffInSeconds = DateIntervalHelpers.CalculateHoursExcludingRange(startAt, endAt,
                    startOfExcludedTime, endOfExcludedTime) * 3600;

                return totalDiffInSeconds;
            });

            var totalTimeInSeconds = onlineSecondsOfDriver.Sum(x => x);
            var time = TimeSpan.FromSeconds(totalTimeInSeconds);
            var onlineHours = $"{time.Hours + time.Days * 24:D2}:{time.Minutes:D2}:{time.Seconds:D2}";

            return new ListServantsOnlineHistory
            (
                day.FirstOrDefault()?.ServantDailyStatistic.Servant?.FirstName,
                day.FirstOrDefault()?.ServantDailyStatistic.Servant?.LastName,
                day.FirstOrDefault()?.ServantDailyStatistic.ServantId ?? 0,
                onlineHours,
                totalTimeInSeconds
            );
        }).MaxBy(x => x.TotalTimeInSeconds);

        return response;
    }


    public async Task<List<ListServantsOnlineHistory>?> ListServantsOnlineHistory(
        ListServantsOnlineHistoryRequest model)
    {
        var excludeStartHour = model.ExcludeStartHour ?? null;
        var excludeEndHour = model.ExcludeEndHour ?? null;
        IQueryable<ServantDailyOnlinePeriod> query;

        query = GetServantOnlineHistoryQuery(model);

        var onlineHistory = await query
            .OrderByDescending(x => x.StartAt)
            .GroupBy(x => x.ServantDailyStatistic.ServantId)
            .Select(x => x.ToList())
            .ToListAsync();

        var response = onlineHistory.Select(day =>
            {
                var onlineSecondsOfDriver = day.Select(x =>
                {
                    var startAt = x.StartAt;
                    var endAt = x.EndAt;

                    var startOfExcludedTime =
                        new DateTime(startAt.Year, startAt.Month, startAt.Day, excludeStartHour ?? 0, 0, 0, 0);
                    var endOfExcludedTime =
                        new DateTime(startAt.Year, startAt.Month, startAt.Day, excludeEndHour ?? 0, 0, 0, 0);


                    var totalDiffInSeconds = DateIntervalHelpers.CalculateHoursExcludingRange(startAt, endAt,
                        startOfExcludedTime, endOfExcludedTime) * 3600;

                    return totalDiffInSeconds;
                });

                var totalTimeInSeconds = onlineSecondsOfDriver.Sum(x => x);
                var time = TimeSpan.FromSeconds(totalTimeInSeconds);
                var onlineHours = $"{time.Hours + time.Days * 24:D2}:{time.Minutes:D2}:{time.Seconds:D2}";

                return new ListServantsOnlineHistory
                (
                    day.FirstOrDefault()?.ServantDailyStatistic.Servant?.FirstName,
                    day.FirstOrDefault()?.ServantDailyStatistic.Servant?.LastName,
                    day.FirstOrDefault()?.ServantDailyStatistic.ServantId ?? 0,
                    onlineHours,
                    totalTimeInSeconds
                );
            })
            .OrderByDescending(x => x.TotalTimeInSeconds);

        return response
            .Where(x => x.TotalTimeInSeconds / 3600 > model.MinHours)
            .ToList();
    }

    private IQueryable<ServantDailyOnlinePeriod> GetServantOnlineHistoryQuery(ListServantsOnlineHistoryRequest request)
    {
        var excludeStartHour = request.ExcludeStartHour ?? null;
        var excludeEndHour = request.ExcludeEndHour ?? null;

        var startOfExcludedTimeSpan = new DateTime(request.StartDate.Year, request.StartDate.Month,
                request.StartDate.Day + 1, excludeStartHour ?? 0, 0, 0, 0)
            .ToUniversalTime();
        var endOfExcludedTimeSpan = new DateTime(request.EndDate.Year, request.EndDate.Month, request.EndDate.Day,
                excludeEndHour ?? 0, 0, 0, 0)
            .ToUniversalTime();


        var query = _context.ServantDailyOnlinePeriods
            .Include(x => x.ServantDailyStatistic)
            .ThenInclude(x => x.Servant)
            .ThenInclude(x => x.User)
            .Where(x => x.StartAt <= request.EndDate)
            .Where(x => x.StartAt >= request.StartDate)
            .Where(x => x.EndAt <= request.EndDate)
            .Where(x => x.EndAt >= request.StartDate)
            .Where(x => x.EndAt != x.StartAt)
            .AsNoTracking();

        if (request.AreaId.HasValue)
            query = query
                .Where(x => x.ServantDailyStatistic.Servant.User.AreaId == request.AreaId);


        if (request.ServantId.HasValue)
            query = query.Where(x => x.ServantDailyStatistic.ServantId == request.ServantId);

        // Below query is to exclude the time span between ExcludeStartHour and ExcludeEndHour-(if they are set)
        if (excludeStartHour is not null && excludeEndHour is not null)
            query = query
                .Where(x =>
                    !(
                        x.StartAt.TimeOfDay >= startOfExcludedTimeSpan.TimeOfDay &&
                        x.EndAt.TimeOfDay >= startOfExcludedTimeSpan.TimeOfDay &&
                        x.StartAt.TimeOfDay <= endOfExcludedTimeSpan.TimeOfDay &&
                        x.EndAt.TimeOfDay <= endOfExcludedTimeSpan.TimeOfDay
                    )
                );

        return query;
    }

    private IQueryable<ServantDailyOnlinePeriod> GetServantOnlinePeriodsQuery(
        GetServantOnlineHistoryRequest request, ulong? servantId = null)
    {
        var excludeStartHour = request.ExcludeStartHour ?? null;
        var excludeEndHour = request.ExcludeEndHour ?? null;

        var startOfExcludedTimeSpan = new DateTime(request.StartDate.Year, request.StartDate.Month,
                request.StartDate.Day + 1, excludeStartHour ?? 0, 0, 0, 0)
            .ToUniversalTime();
        var endOfExcludedTimeSpan = new DateTime(request.EndDate.Year, request.EndDate.Month, request.EndDate.Day,
                excludeEndHour ?? 0, 0, 0, 0)
            .ToUniversalTime();


        var query = _context.ServantDailyOnlinePeriods
            .Include(x => x.ServantDailyStatistic)
            .ThenInclude(x => x.Servant)
            .ThenInclude(x => x.User)
            .Where(x => x.StartAt <= request.EndDate)
            .Where(x => x.StartAt >= request.StartDate)
            .Where(x => x.EndAt <= request.EndDate)
            .Where(x => x.EndAt >= request.StartDate)
            .Where(x => x.EndAt != x.StartAt)
            .AsNoTracking();

        if (request.AreaId.HasValue)
            query = query
                .Where(x => x.ServantDailyStatistic.Servant.User.AreaId == request.AreaId);


        if (servantId.HasValue)
            query = query.Where(x => x.ServantDailyStatistic.ServantId == servantId);

        // Below query is to exclude the time span between ExcludeStartHour and ExcludeEndHour-(if they are set)
        if (excludeStartHour is not null && excludeEndHour is not null)
            query = query
                .Where(x =>
                    !(
                        x.StartAt.TimeOfDay >= startOfExcludedTimeSpan.TimeOfDay &&
                        x.EndAt.TimeOfDay >= startOfExcludedTimeSpan.TimeOfDay &&
                        x.StartAt.TimeOfDay <= endOfExcludedTimeSpan.TimeOfDay &&
                        x.EndAt.TimeOfDay <= endOfExcludedTimeSpan.TimeOfDay
                    )
                );

        return query;
    }
}