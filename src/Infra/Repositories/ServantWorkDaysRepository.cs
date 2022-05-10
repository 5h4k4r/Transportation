using System;
using System.Collections.ObjectModel;
using System.Data;
using AutoMapper;
using Core.Extensions;
using Core.Interfaces;
using Core.Models;
using Infra.Entities;
using Infra.Extensions;
using Infra.Requests;
using Infra.Responses;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;
namespace Infra.Repositories;

public class ServantWorkDaysRepository : IServantWorkDaysRepository
{

    private readonly transportationContext _context;
    private readonly IMapper _mapper;

    public ServantWorkDaysRepository(transportationContext repositoryContext, IMapper mapper)
    {
        _context = repositoryContext;
        _mapper = mapper;
    }

    public async Task<ServantOnlinePeriods> GetServantOnlinePeriods(ulong ServantId, GetServantOnlinePeriodsRequest model)
    {
        var ExcludeStartHour = model.ExcludeStartHour ?? null;
        var ExcludeEndHour = model.ExcludeEndHour ?? null;
        var today = DateTime.UtcNow;

        var startOfExcludedTimeSpan = new DateTime(today.Year, today.Month, today.Day, ExcludeStartHour ?? 0, 0, 0, 0).ToUniversalTime().TimeOfDay;
        var endOfExcludedTimeSpan = new DateTime(today.Year, today.Month, today.Day, ExcludeEndHour ?? 0, 0, 0, 0).ToUniversalTime().TimeOfDay;

        var query = _context.ServantDailyOnlinePeriods.Include(x => x.ServantDailyStatistic).ThenInclude(x => x.Servant)
        .Where(x => x.ServantDailyStatistic.ServantId == ServantId)
        .Where(x => x.StartAt <= model.EndDate)
        .Where(x => x.StartAt >= model.StartDate)
        .Where(x => x.EndAt <= model.EndDate)
        .Where(x => x.EndAt >= model.StartDate)
        .Where(x => x.EndAt != x.StartAt);

        // Below query is to exclude the time span between ExcludeStartHour and ExcludeEndHour-(if they are set)
        if (ExcludeStartHour != null && ExcludeEndHour != null)
            query = query
            .Where(x =>
                !(
                x.StartAt.TimeOfDay >= startOfExcludedTimeSpan &&
                x.EndAt.TimeOfDay >= startOfExcludedTimeSpan &&
                x.StartAt.TimeOfDay <= endOfExcludedTimeSpan &&
                x.EndAt.TimeOfDay <= endOfExcludedTimeSpan
                )
            );

        var DailyStats = await query
           .OrderByDescending(x => x.StartAt)
           .GroupBy(x => x.ServantDailyStatistic.DayId)
           .Select(x => x.ToList())
           .AsNoTracking()
           .ApplyPagination(model)
           .ToListAsync();


        var ServantWorkDayPeriods = DailyStats.Select(WorkDay =>
        {
            var Hours = WorkDay.Select(WorkingPeriod =>
            {

                var StartAt = WorkingPeriod.StartAt;
                var EndAt = WorkingPeriod.EndAt;

                if (ExcludeStartHour != null && ExcludeEndHour != null)
                {
                    var startOfExcludedTime = new DateTime(StartAt.Year, StartAt.Month, StartAt.Day, ExcludeStartHour ?? 0, 0, 0, 0).ToUniversalTime();
                    var endOfExcludedTime = new DateTime(StartAt.Year, StartAt.Month, StartAt.Day, ExcludeEndHour ?? 0, 0, 0, 0).ToUniversalTime();

                    if (StartAt.TimeOfDay < endOfExcludedTime.TimeOfDay && StartAt.TimeOfDay > startOfExcludedTime.TimeOfDay)
                        StartAt = endOfExcludedTime;

                    if (EndAt.TimeOfDay > startOfExcludedTime.TimeOfDay && EndAt.TimeOfDay < endOfExcludedTime.TimeOfDay)
                        EndAt = startOfExcludedTime;

                }

                var Difference = EndAt.Subtract(StartAt);
                var TotalDiffInSeconds = Difference.TotalSeconds;
                TimeSpan DiffInTime = TimeSpan.FromSeconds((double)TotalDiffInSeconds);

                return new ServantOnlinePeriodItem
                {
                    StartAt = StartAt.AddHours(3),
                    EndAt = EndAt.AddHours(3),
                    DiffInTime = DiffInTime,
                    TotalPeriodInSeconds = TotalDiffInSeconds
                };

            }).OrderBy(x => x.StartAt);

            var TotalTimeInSeconds = Hours.Sum(x => x.TotalPeriodInSeconds);
            TimeSpan DiffInTime = TimeSpan.FromSeconds(Hours.Sum(x => x.TotalPeriodInSeconds));


            return new ServantOnlinePeriod
            {
                TotalOnlineTime = DiffInTime,
                Date = WorkDay?.FirstOrDefault()?.StartAt.Date,
                Periods = Hours
            };

        });


        var totalSeconds = ServantWorkDayPeriods.Sum(x => x.Periods.Sum(x => x.DiffInTime.TotalSeconds));


        TimeSpan time = TimeSpan.FromSeconds((double)totalSeconds);

        //here backslash is must to tell that colon is
        //not the part of format, it just a character that we want in output
        string answer = string.Format("{0:D2}:{1:D2}:{2:D2}",
                time.Hours + (time.Days * 24),
                time.Minutes,
                time.Seconds);

        return new ServantOnlinePeriods
        {
            TotalTimeInSeconds = totalSeconds,
            TotalTime = answer,
            Items = ServantWorkDayPeriods
        };


    }

    public Task<int> GetServantOnlinePeriodsCount(ulong ServantId, GetServantOnlinePeriodsRequest model)
    {
        var ExcludeStartHour = model.ExcludeStartHour ?? null;
        var ExcludeEndHour = model.ExcludeEndHour ?? null;
        var today = DateTime.UtcNow;

        var startOfExcludedTimeSpan = new DateTime(today.Year, today.Month, today.Day, ExcludeStartHour ?? 0, 0, 0, 0).ToUniversalTime().TimeOfDay;
        var endOfExcludedTimeSpan = new DateTime(today.Year, today.Month, today.Day, ExcludeEndHour ?? 0, 0, 0, 0).ToUniversalTime().TimeOfDay;

        var query = _context.ServantDailyOnlinePeriods
        .Include(x => x.ServantDailyStatistic)
        .ThenInclude(x => x.Servant)
        .Where(x => x.ServantDailyStatistic.ServantId == ServantId)
        .Where(x => x.StartAt <= model.EndDate)
        .Where(x => x.StartAt >= model.StartDate)
        .Where(x => x.EndAt <= model.EndDate)
        .Where(x => x.EndAt >= model.StartDate)
        .Where(x => x.EndAt != x.StartAt);

        // Below query is to exclude the time span between ExcludeStartHour and ExcludeEndHour-(if they are set)
        if (ExcludeStartHour != null && ExcludeEndHour != null)
            query = query
            .Where(x =>
                !(
                x.StartAt.TimeOfDay >= startOfExcludedTimeSpan &&
                x.EndAt.TimeOfDay >= startOfExcludedTimeSpan &&
                x.StartAt.TimeOfDay <= endOfExcludedTimeSpan &&
                x.EndAt.TimeOfDay <= endOfExcludedTimeSpan
                )
            );

        var count = query
           .OrderByDescending(x => x.StartAt)
           .GroupBy(x => x.ServantDailyStatistic.DayId)
           .AsNoTracking()
           .CountAsync();


        return count;
    }
    public async Task<List<ListServantsOnlineHistory>?> ListServantsOnlineHistory(ListServantsOnlineHistoryRequest model)
    {

        var ExcludeStartHour = model.ExcludeStartHour ?? null;
        var ExcludeEndHour = model.ExcludeEndHour ?? null;
        var today = DateTime.UtcNow;

        var startOfExcludedTimeSpan = new DateTime(today.Year, today.Month, today.Day, ExcludeStartHour ?? 0, 0, 0, 0).ToUniversalTime().TimeOfDay;
        var endOfExcludedTimeSpan = new DateTime(today.Year, today.Month, today.Day, ExcludeEndHour ?? 0, 0, 0, 0).ToUniversalTime().TimeOfDay;


        var query = _context.ServantDailyOnlinePeriods
        .Include(x => x.ServantDailyStatistic)
        .ThenInclude(x => x.Servant)
        .Where(x => x.StartAt <= model.EndDate)
        .Where(x => x.StartAt >= model.StartDate)
        .Where(x => x.EndAt <= model.EndDate)
        .Where(x => x.EndAt >= model.StartDate)
        .Where(x => x.EndAt != x.StartAt);


        if (ExcludeStartHour is not null && ExcludeEndHour is not null)
            query = query
            .Where(x =>
                !(
                x.StartAt.TimeOfDay >= startOfExcludedTimeSpan &&
                x.EndAt.TimeOfDay >= startOfExcludedTimeSpan &&
                x.StartAt.TimeOfDay <= endOfExcludedTimeSpan &&
                x.EndAt.TimeOfDay <= endOfExcludedTimeSpan
                )
            );
        var onlineHistory = await query
            .OrderByDescending(x => x.StartAt)
            .GroupBy(x => x.ServantDailyStatistic.ServantId)
            .ApplyPagination(model)
            .Select(x => x.ToList())
            .AsNoTracking()
            .ToListAsync();

        var response = onlineHistory.Select(day =>
        {

            var onlineSecondsOfDriver = day.Select(x =>
            {
                var StartAt = x.StartAt;
                var EndAt = x.EndAt;

                if (ExcludeStartHour != null && ExcludeEndHour != null)
                {
                    var startOfExcludedTime = new DateTime(StartAt.Year, StartAt.Month, StartAt.Day, ExcludeStartHour ?? 0, 0, 0, 0).ToUniversalTime();
                    var endOfExcludedTime = new DateTime(StartAt.Year, StartAt.Month, StartAt.Day, ExcludeEndHour ?? 0, 0, 0, 0).ToUniversalTime();

                    if (StartAt.TimeOfDay < endOfExcludedTime.TimeOfDay && StartAt.TimeOfDay > startOfExcludedTime.TimeOfDay)
                        StartAt = endOfExcludedTime;

                    if (EndAt.TimeOfDay > startOfExcludedTime.TimeOfDay && EndAt.TimeOfDay < endOfExcludedTime.TimeOfDay)
                        EndAt = startOfExcludedTime;

                }


                var Difference = EndAt.Subtract(StartAt);
                var TotalDiffInSeconds = Difference.TotalSeconds;


                return TotalDiffInSeconds;

            });

            var totalTimeInSeconds = onlineSecondsOfDriver.Sum(x => x);
            var time = TimeSpan.FromSeconds(totalTimeInSeconds);
            string OnlineHours = string.Format("{0:D2}:{1:D2}:{2:D2}",
            time.Hours + (time.Days * 24),
            time.Minutes,
            time.Seconds);

            return new ListServantsOnlineHistory
            (
                day.FirstOrDefault()?.ServantDailyStatistic.Servant.FirstName,
                day.FirstOrDefault()?.ServantDailyStatistic.Servant.LastName,
                day.FirstOrDefault()?.ServantDailyStatistic.ServantId ?? (ulong)0,
                OnlineHours,
                totalTimeInSeconds

            );

        }).OrderByDescending(x => x.OnlineHours);

        return response.ToList();

    }
    public async Task<int> ListServantsOnlineHistoryCount(ListServantsOnlineHistoryRequest model)
    {

        var ExcludeStartHour = model.ExcludeStartHour ?? null;
        var ExcludeEndHour = model.ExcludeEndHour ?? null;
        var today = DateTime.UtcNow;

        var startOfExcludedTimeSpan = new DateTime(today.Year, today.Month, today.Day, ExcludeStartHour ?? 0, 0, 0, 0).ToUniversalTime().TimeOfDay;
        var endOfExcludedTimeSpan = new DateTime(today.Year, today.Month, today.Day, ExcludeEndHour ?? 0, 0, 0, 0).ToUniversalTime().TimeOfDay;



        var query = _context.ServantDailyOnlinePeriods
        .Include(x => x.ServantDailyStatistic)
        .ThenInclude(x => x.Servant)
        .Where(x => x.StartAt <= model.EndDate)
        .Where(x => x.StartAt >= model.StartDate)
        .Where(x => x.EndAt <= model.EndDate)
        .Where(x => x.EndAt >= model.StartDate)
        .Where(x => x.EndAt != x.StartAt);


        if (ExcludeStartHour is not null && ExcludeEndHour is not null)
            query = query
            .Where(x =>
                !(
                x.StartAt.TimeOfDay >= startOfExcludedTimeSpan &&
                x.EndAt.TimeOfDay >= startOfExcludedTimeSpan &&
                x.StartAt.TimeOfDay <= endOfExcludedTimeSpan &&
                x.EndAt.TimeOfDay <= endOfExcludedTimeSpan
                )
            );

        var onlineHistory = await query
            .OrderByDescending(x => x.StartAt)
            .GroupBy(x => x.ServantDailyStatistic.ServantId)
            .CountAsync();

        return onlineHistory;
    }

}