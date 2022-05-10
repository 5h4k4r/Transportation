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

        var query = _context.ServantDailyStatistics
        .Where(x => x.ServantId == ServantId)
        .Join(
            _context.ServantDailyOnlinePeriods,
            ServantDailyStatistics => ServantDailyStatistics.Id,
            ServantDailyOnlinePeriods => ServantDailyOnlinePeriods.ServantDailyStatisticId,
            (ServantDailyStatistic, ServantDailyOnlinePeriod) => new
            {
                ServantDailyStatistic,
                ServantDailyOnlinePeriod
            }
        )
        .Where(x => x.ServantDailyOnlinePeriod.StartAt <= model.EndDate)
        .Where(x => x.ServantDailyOnlinePeriod.StartAt >= model.StartDate)
        .Where(x => x.ServantDailyOnlinePeriod.EndAt <= model.EndDate)
        .Where(x => x.ServantDailyOnlinePeriod.EndAt >= model.StartDate)
        .Where(x => x.ServantDailyOnlinePeriod.EndAt != x.ServantDailyOnlinePeriod.StartAt);

        // Below query is to exclude the time span between ExcludeStartHour and ExcludeEndHour-(if they are set)
        if (ExcludeStartHour != null && ExcludeEndHour != null)
            query = query
            .Where(x =>
                !(
                x.ServantDailyOnlinePeriod.StartAt.TimeOfDay >= startOfExcludedTimeSpan &&
                x.ServantDailyOnlinePeriod.EndAt.TimeOfDay >= startOfExcludedTimeSpan &&
                x.ServantDailyOnlinePeriod.StartAt.TimeOfDay <= endOfExcludedTimeSpan &&
                x.ServantDailyOnlinePeriod.EndAt.TimeOfDay <= endOfExcludedTimeSpan
                )
            );

        var DailyStats = await query
           .OrderByDescending(x => x.ServantDailyOnlinePeriod.StartAt)
           .GroupBy(x => x.ServantDailyStatistic.DayId)
           .Select(x => x.ToList())
           .AsNoTracking()
           .ApplyPagination(model)
           .ToListAsync();


        var ServantWorkDayPeriods = DailyStats.Select(WorkDay =>
        {
            var Hours = WorkDay.Select(WorkingPeriod =>
            {

                var StartAt = WorkingPeriod.ServantDailyOnlinePeriod.StartAt;
                var EndAt = WorkingPeriod.ServantDailyOnlinePeriod.EndAt;

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
                    TotalDiffInSeconds = TotalDiffInSeconds
                };

            }).OrderBy(x => x.StartAt);

            TimeSpan DiffInTime = TimeSpan.FromSeconds(Hours.Sum(x => x.TotalDiffInSeconds));

            return new ServantOnlinePeriod
            {
                TotalOnlineTimeInDay = DiffInTime,
                Date = WorkDay?.FirstOrDefault()?.ServantDailyOnlinePeriod.StartAt.Date,
                Periods = Hours
            };

        });


        var totalSeconds = ServantWorkDayPeriods.Sum(x => x.Periods.Sum(x => x.DiffInTime.TotalSeconds));


        TimeSpan time = TimeSpan.FromSeconds((double)totalSeconds);

        //here backslash is must to tell that colon is
        //not the part of format, it just a character that we want in output
        string totalTimeInString = time.ToString(@"hh\:mm\:ss\:fff");

        return new ServantOnlinePeriods
        {
            TotalTime = time,
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

        var query = _context.ServantDailyStatistics
        .Where(x => x.ServantId == ServantId)
        .Join(
            _context.ServantDailyOnlinePeriods,
            ServantDailyStatistics => ServantDailyStatistics.Id,
            ServantDailyOnlinePeriods => ServantDailyOnlinePeriods.ServantDailyStatisticId,
            (ServantDailyStatistic, ServantDailyOnlinePeriod) => new
            {
                ServantDailyStatistic,
                ServantDailyOnlinePeriod
            }
        )
        .Where(x => x.ServantDailyOnlinePeriod.StartAt <= model.EndDate)
        .Where(x => x.ServantDailyOnlinePeriod.StartAt >= model.StartDate)
        .Where(x => x.ServantDailyOnlinePeriod.EndAt <= model.EndDate)
        .Where(x => x.ServantDailyOnlinePeriod.EndAt >= model.StartDate)
        .Where(x => x.ServantDailyOnlinePeriod.EndAt != x.ServantDailyOnlinePeriod.StartAt);

        // Below query is to exclude the time span between ExcludeStartHour and ExcludeEndHour-(if they are set)
        if (ExcludeStartHour != null && ExcludeEndHour != null)
            query = query
            .Where(x =>
                !(
                x.ServantDailyOnlinePeriod.StartAt.TimeOfDay >= startOfExcludedTimeSpan &&
                x.ServantDailyOnlinePeriod.EndAt.TimeOfDay >= startOfExcludedTimeSpan &&
                x.ServantDailyOnlinePeriod.StartAt.TimeOfDay <= endOfExcludedTimeSpan &&
                x.ServantDailyOnlinePeriod.EndAt.TimeOfDay <= endOfExcludedTimeSpan
                )
            );

        var count = query
           .OrderByDescending(x => x.ServantDailyOnlinePeriod.StartAt)
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

        var query = _context.ServantDailyStatistics
        .Include(x => x.Servant)
        .Join(
             _context.ServantDailyOnlinePeriods,
             ServantDailyStatistics => ServantDailyStatistics.Id,
             ServantDailyOnlinePeriods => ServantDailyOnlinePeriods.ServantDailyStatisticId,
             (ServantDailyStatistic, ServantDailyOnlinePeriod) => new
             {
                 ServantDailyStatistic,
                 ServantDailyOnlinePeriod
             }
        )
        .Where(x => x.ServantDailyOnlinePeriod.StartAt <= model.EndDate)
        .Where(x => x.ServantDailyOnlinePeriod.StartAt >= model.StartDate)
        .Where(x => x.ServantDailyOnlinePeriod.EndAt <= model.EndDate)
        .Where(x => x.ServantDailyOnlinePeriod.EndAt >= model.StartDate)
        .Where(x => x.ServantDailyOnlinePeriod.EndAt != x.ServantDailyOnlinePeriod.StartAt);


        if (ExcludeStartHour is not null && ExcludeEndHour is not null)
            query = query
            .Where(x =>
                !(
                x.ServantDailyOnlinePeriod.StartAt.TimeOfDay >= startOfExcludedTimeSpan &&
                x.ServantDailyOnlinePeriod.EndAt.TimeOfDay >= startOfExcludedTimeSpan &&
                x.ServantDailyOnlinePeriod.StartAt.TimeOfDay <= endOfExcludedTimeSpan &&
                x.ServantDailyOnlinePeriod.EndAt.TimeOfDay <= endOfExcludedTimeSpan
                )
            );
        var onlineHistory = await query
            .OrderByDescending(x => x.ServantDailyOnlinePeriod.StartAt)
            .GroupBy(x => x.ServantDailyStatistic.ServantId)
            .Select(x => x.ToList())
            .ApplyPagination(model)
            .AsNoTracking()
            .ToListAsync();

        var response = onlineHistory.Select(day =>
        {

            var onlineSecondsOfDriver = day.Select(x =>
            {
                var StartAt = x.ServantDailyOnlinePeriod.StartAt;
                var EndAt = x.ServantDailyOnlinePeriod.EndAt;

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

                return
                    TotalDiffInSeconds;

                // // count all online time for each day except 00:00 - 06:00
                // var totalSeconds = x.ServantDailyOnlinePeriod.EndAt.Subtract(x.ServantDailyOnlinePeriod.StartAt).TotalSeconds;
                // return totalSeconds;

            });


            return new ListServantsOnlineHistory
            (
                day.FirstOrDefault()?.ServantDailyStatistic.Servant.FirstName,
                day.FirstOrDefault()?.ServantDailyStatistic.Servant.LastName,
                day.FirstOrDefault()?.ServantDailyStatistic.ServantId ?? (ulong)0,
                TimeSpan.FromSeconds(onlineSecondsOfDriver.Sum(x => x))

            );

        }).OrderByDescending(x => x.OnlineHours);

        return response.ToList();

    }
    public async Task<int> ListServantsOnlineHistoryCount(ListServantsOnlineHistoryRequest model)
    {

        var servantsCount = await _context.ServantDailyStatistics
        .Join(
            _context.Servants,
            ServantDailyStatistics => ServantDailyStatistics.ServantId,
            Servants => (ulong)Servants.UserId,
            (ServantDailyStatistics, Servants) => new
            {
                ServantDailyStatistics,
                Servants
            }
        )
        .Where(x => x.ServantDailyStatistics.CreatedAt >= model.StartDate && x.ServantDailyStatistics.CreatedAt <= model.EndDate)
        .CountAsync();

        return servantsCount;
    }

}