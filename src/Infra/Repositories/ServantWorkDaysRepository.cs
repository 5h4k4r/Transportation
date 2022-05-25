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

    public async Task<ServantOnlinePeriods> GetServantOnlinePeriods(ulong servantId, GetServantOnlinePeriodsRequest model)
    {
        var excludeStartHour = model.ExcludeStartHour ;
        var excludeEndHour = model.ExcludeEndHour;
        
        var dailyStats = await GetServantOnlinePeriodsQuery(model, servantId)
           .OrderByDescending(x => x.StartAt)
           .GroupBy(x => x.ServantDailyStatistic.DayId)
           .Select(x => x.ToList())
           .ApplyPagination(model)
           .ToListAsync();


        var servantWorkDayPeriods = dailyStats.Select(workDay =>
        {
            var hours = workDay.Select(workingPeriod =>
            {

                var startAt = workingPeriod.StartAt;
                var endAt = workingPeriod.EndAt;

                if (excludeStartHour != null && excludeEndHour != null)
                {
                    var startOfExcludedTime = new DateTime(startAt.Year, startAt.Month, startAt.Day, (int)excludeStartHour, 0, 0, 0).ToUniversalTime();
                    var endOfExcludedTime = new DateTime(startAt.Year, startAt.Month, startAt.Day, (int)excludeEndHour, 0, 0, 0).ToUniversalTime();

                    if (startAt.TimeOfDay < endOfExcludedTime.TimeOfDay && startAt.TimeOfDay > startOfExcludedTime.TimeOfDay)
                        startAt = endOfExcludedTime;

                    if (endAt.TimeOfDay > startOfExcludedTime.TimeOfDay && endAt.TimeOfDay < endOfExcludedTime.TimeOfDay)
                        endAt = startOfExcludedTime;

                }

                var difference = endAt.Subtract(startAt);
                var totalDiffInSeconds = difference.TotalSeconds;
                var diffInTime = TimeSpan.FromSeconds(totalDiffInSeconds);

                return new ServantOnlinePeriodItem
                {
                    StartAt = startAt.AddHours(3),
                    EndAt = endAt.AddHours(3),
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


        var totalSeconds = servantWorkDayPeriods.Sum(x => x.Periods.Sum(item => item.DiffInTime.TotalSeconds));


        var time = TimeSpan.FromSeconds(totalSeconds);

        //here backslash is must to tell that colon is
        //not the part of format, it just a character that we want in output
        var answer = $"{time.Hours + (time.Days * 24):D2}:{time.Minutes:D2}:{time.Seconds:D2}";

        return new ServantOnlinePeriods
        {
            TotalTimeInSeconds = totalSeconds,
            TotalTime = answer,
            Items = servantWorkDayPeriods
        };


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

                if (excludeStartHour != null && excludeEndHour != null)
                {
                    var startOfExcludedTime = new DateTime(startAt.Year, startAt.Month, startAt.Day, (int)excludeStartHour, 0, 0, 0).ToUniversalTime();
                    var endOfExcludedTime = new DateTime(startAt.Year, startAt.Month, startAt.Day, (int)excludeEndHour, 0, 0, 0).ToUniversalTime();

                    if (startAt.TimeOfDay < endOfExcludedTime.TimeOfDay && startAt.TimeOfDay > startOfExcludedTime.TimeOfDay)
                        startAt = endOfExcludedTime;

                    if (endAt.TimeOfDay > startOfExcludedTime.TimeOfDay && endAt.TimeOfDay < endOfExcludedTime.TimeOfDay)
                        endAt = startOfExcludedTime;

                }


                var difference = endAt.Subtract(startAt);
                var totalDiffInSeconds = difference.TotalSeconds;


                return totalDiffInSeconds;

            });

            var totalTimeInSeconds = onlineSecondsOfDriver.Sum(x => x);
            var time = TimeSpan.FromSeconds(totalTimeInSeconds);
            var onlineHours = string.Format("{0:D2}:{1:D2}:{2:D2}",
            time.Hours + (time.Days * 24),
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

        return response.ToList();

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

    private IQueryable<ServantDailyOnlinePeriod> GetServantOnlinePeriodsQuery(GetServantOnlinePeriodsRequest request, ulong? servantId = null)
    {

        var excludeStartHour = request.ExcludeStartHour ?? null;
        var excludeEndHour = request.ExcludeEndHour ?? null;
        var today = DateTime.UtcNow;

        var startOfExcludedTimeSpan = new DateTime(today.Year, today.Month, today.Day, excludeStartHour ?? 0, 0, 0, 0).ToUniversalTime().TimeOfDay;
        var endOfExcludedTimeSpan = new DateTime(today.Year, today.Month, today.Day, excludeEndHour ?? 0, 0, 0, 0).ToUniversalTime().TimeOfDay;


        var query = _context.ServantDailyOnlinePeriods.Include(x => x.ServantDailyStatistic).ThenInclude(x => x.Servant)
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
                x.StartAt.TimeOfDay >= startOfExcludedTimeSpan &&
                x.EndAt.TimeOfDay >= startOfExcludedTimeSpan &&
                x.StartAt.TimeOfDay <= endOfExcludedTimeSpan &&
                x.EndAt.TimeOfDay <= endOfExcludedTimeSpan
                )
            );

        return query;
    }

}