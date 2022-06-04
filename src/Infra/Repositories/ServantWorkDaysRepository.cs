using Core.Helpers;
using Core.Interfaces;
using Core.Models.Repositories;
using Core.Models.Requests;
using Infra.Entities;
using Infra.Extensions;
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
        GetServantOnlinePeriodsRequest model)
    {
        var excludeStartHour = model.ExcludeStartHour;
        var excludeEndHour = model.ExcludeEndHour;

        var dailyStats = await GetServantOnlinePeriodsQuery(model, servantId)
            .OrderByDescending(x => x.StartAt)
            .GroupBy(x => x.ServantDailyStatistic.DayId)
            .Select(x => x.ToList())
            // .ApplyPagination(model)
            .ToListAsync();


        var servantWorkDayPeriods = dailyStats.Select(workDay =>
        {
            var hours = workDay.Select(workingPeriod =>
            {
                var startAt = workingPeriod.StartAt;
                var endAt = workingPeriod.EndAt;

                // if (excludeStartHour != null && excludeEndHour != null)
                // {
                var startOfExcludedTime =
                    new DateTime(startAt.Year, startAt.Month, startAt.Day, excludeStartHour ?? 0, 0, 0, 0);

                var endOfExcludedTime =
                    new DateTime(startAt.Year, startAt.Month, startAt.Day, excludeEndHour ?? 0, 0, 0, 0);


                //  var dateDiff = startAt.Day - startOfExcludedTime.Day;
                //  var dateDiff2 = endAt.Day - endOfExcludedTime.Day;
                //
                // startOfExcludedTime =  startOfExcludedTime.AddDays(dateDiff);
                // endOfExcludedTime = endOfExcludedTime.AddDays(dateDiff2+1);
                //
                //  if (startAt < endOfExcludedTime && startAt > startOfExcludedTime)
                //  startAt =  new DateTime(startAt.Year, startAt.Month, startAt.Day, 0, 0, 0, 0)
                //      .ToUniversalTime().AddSeconds(endOfExcludedTime.TimeOfDay.TotalSeconds);
                //
                //
                //  if (endAt > startOfExcludedTime && endAt < endOfExcludedTime)
                //      endAt =  new DateTime(endAt.Year, endAt.Month, endAt.Day, 0, 0, 0, 0)
                //          .ToUniversalTime().AddSeconds(startOfExcludedTime.TimeOfDay.TotalSeconds);

                // }
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
            }).OrderBy(x => x.StartAt).ToList();

            var totalTimeInSeconds = hours.Sum(x => x.TotalPeriodInSeconds);
            var diffInTime = TimeSpan.FromSeconds(totalTimeInSeconds);


            return new ServantOnlinePeriod
            {
                TotalOnlineTime = diffInTime,
                Date = workDay.FirstOrDefault()?.StartAt.Date,
                Periods = hours
            };
        }).ToList();


        return servantWorkDayPeriods;
    }

    public Task<int> GetServantOnlinePeriodsCount(ulong servantId, GetServantOnlinePeriodsRequest model)
    {
        var query = GetServantOnlinePeriodsQuery(model, servantId);

        var count = query
            .OrderByDescending(x => x.StartAt)
            .GroupBy(x => x.ServantDailyStatistic.DayId)
            .CountAsync();


        return count;
    }

    public async Task<List<ListServantsOnlineHistory>?> ListServantsOnlineHistory(GetServantOnlinePeriodsRequest model)
    {
        var excludeStartHour = model.ExcludeStartHour ?? null;
        var excludeEndHour = model.ExcludeEndHour ?? null;

        var query = GetServantOnlinePeriodsQuery(model);

        var onlineHistory = await query
            .OrderByDescending(x => x.StartAt)
            .GroupBy(x => x.ServantDailyStatistic.ServantId)
            .ApplyPagination(model)
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
            var onlineHours = string.Format("{0:D2}:{1:D2}:{2:D2}",
                time.Hours + time.Days * 24,
                time.Minutes,
                time.Seconds);

            return new ListServantsOnlineHistory
            (
                day.FirstOrDefault()?.ServantDailyStatistic.Servant?.FirstName,
                day.FirstOrDefault()?.ServantDailyStatistic.Servant?.LastName,
                day.FirstOrDefault()?.ServantDailyStatistic.ServantId ?? 0,
                onlineHours,
                totalTimeInSeconds
            );
        }).OrderByDescending(x => x.OnlineHours);

        return response.OrderByDescending(x => x.TotalTimeInSeconds).ToList();
    }

    public async Task<int> ListServantsOnlineHistoryCount(GetServantOnlinePeriodsRequest model)
    {
        var query = GetServantOnlinePeriodsQuery(model);


        var onlineHistory = await query
            .OrderByDescending(x => x.StartAt)
            .GroupBy(x => x.ServantDailyStatistic.ServantId)
            .CountAsync();

        return onlineHistory;
    }

    private IQueryable<ServantDailyOnlinePeriod> GetServantOnlinePeriodsQuery(GetServantOnlinePeriodsRequest request,
        ulong? servantId = null)
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
            .Where(x => x.ServantDailyStatistic.Servant.User.AreaId == request.AreaId)
            .Where(x => x.StartAt <= request.EndDate)
            .Where(x => x.StartAt >= request.StartDate)
            .Where(x => x.EndAt <= request.EndDate)
            .Where(x => x.EndAt >= request.StartDate)
            .Where(x => x.EndAt != x.StartAt)
            .AsNoTracking();

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