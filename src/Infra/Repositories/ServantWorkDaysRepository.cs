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

    public async Task<ServantWorkDays> GetServantWorkDays(ulong ServantId, ServantWorkDaysRequest model)
    {
        var ExcludeStartHour = model.ExcludeStartHour ?? null;
        var ExcludeEndHour = model.ExcludeEndHour ?? null;
        var today = DateTime.UtcNow;

        var startOfExcludedTime = new DateTime(today.Year, today.Month, today.Day, ExcludeStartHour ?? 0, 0, 0, 0).ToUniversalTime();
        var endOfExcludedTime = new DateTime(today.Year, today.Month, today.Day, ExcludeEndHour ?? 0, 0, 0, 0).ToUniversalTime();
        // ExcludeStartHour = ExcludeStartHour <= 3 ? 24 - 3 : ExcludeStartHour - 3;

        // ExcludeEndHour = ExcludeEndHour <= 3 ? 24 - 3 : ExcludeEndHour - 3;

        var DailyStats = await _context.ServantDailyStatistics
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
        .Where(x => x.ServantDailyOnlinePeriod.EndAt != x.ServantDailyOnlinePeriod.StartAt)
        .Where(x => !(x.ServantDailyOnlinePeriod.StartAt >= startOfExcludedTime && x.ServantDailyOnlinePeriod.StartAt <= endOfExcludedTime) &&
        !(x.ServantDailyOnlinePeriod.EndAt >= startOfExcludedTime && x.ServantDailyOnlinePeriod.EndAt <= endOfExcludedTime)
        )
        .OrderByDescending(x => x.ServantDailyOnlinePeriod.StartAt)
        .GroupBy(x => x.ServantDailyStatistic.DayId)
        .Select(x => x.ToList())
        .AsNoTracking()
        .ApplyPagination(model)
        .ToListAsync();


        var ServantWorkDayPeriods = DailyStats.Select(WorkDay =>
        {
            var Hours = WorkDay.Select(WorkingHours =>
            {

                var StartAt = WorkingHours.ServantDailyOnlinePeriod.StartAt;
                var EndAt = WorkingHours.ServantDailyOnlinePeriod.EndAt;

                var startOfExcludedTime = new DateTime(StartAt.Year, StartAt.Month, StartAt.Day, ExcludeStartHour ?? 0, 0, 0, 0).ToUniversalTime();
                var endOfExcludedTime = new DateTime(StartAt.Year, StartAt.Month, StartAt.Day, ExcludeEndHour ?? 0, 0, 0, 0).ToUniversalTime();

                if (StartAt.Hour < endOfExcludedTime.Hour)
                    StartAt = endOfExcludedTime;

                if (EndAt.Hour > startOfExcludedTime.Hour && EndAt.Hour < endOfExcludedTime.Hour)
                    EndAt = startOfExcludedTime;


                var Difference = EndAt.Subtract(StartAt);
                var TotalDiffInSeconds = Difference.TotalSeconds;
                TimeSpan DiffInTime = TimeSpan.FromSeconds((double)TotalDiffInSeconds);

                return new ServantWorkDayPeriodItem
                {
                    StartAt = StartAt.AddHours(3),
                    EndAt = EndAt.AddHours(3),
                    DiffInTime = DiffInTime,
                    TotalDiffInSeconds = TotalDiffInSeconds
                };

            }).OrderBy(x => x.StartAt);

            TimeSpan DiffInTime = TimeSpan.FromSeconds((double)Hours.Sum(x => x.TotalDiffInSeconds));

            return new ServantWorkDay
            {
                Date = WorkDay?.FirstOrDefault()?.ServantDailyOnlinePeriod.StartAt.Date,
                Periods = Hours,
                TotalOnlineTimeInDay = DiffInTime
            };

        });


        var totalSeconds = ServantWorkDayPeriods.Sum(x => x.Periods.Sum(x => x.DiffInTime.TotalSeconds));


        TimeSpan time = TimeSpan.FromSeconds((double)totalSeconds);

        //here backslash is must to tell that colon is
        //not the part of format, it just a character that we want in output
        string totalTimeInString = time.ToString(@"hh\:mm\:ss\:fff");

        return new ServantWorkDays
        {
            TotalTime = totalTimeInString,
            Items = ServantWorkDayPeriods
        };


    }

    public Task<int> GetServantWorkDaysCount(ulong ServantId, ServantWorkDaysRequest model)
    {
        var ExcludeStartHour = model.ExcludeStartHour ?? null;
        var ExcludeEndHour = model.ExcludeEndHour ?? null;

        var count = _context.ServantDailyStatistics
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
         .Where(x => x.ServantDailyOnlinePeriod.EndAt != x.ServantDailyOnlinePeriod.StartAt)
         .Where(x => !(x.ServantDailyOnlinePeriod.StartAt.Hour + 3 >= 0 && x.ServantDailyOnlinePeriod.StartAt.Hour + 3 <= 6) &&
         !(x.ServantDailyOnlinePeriod.EndAt.Hour + 3 >= 0 && x.ServantDailyOnlinePeriod.EndAt.Hour + 3 <= 6)
         )
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

        var dailyStats = await _context.ServantDailyStatistics
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
        .Where(x => x.ServantDailyOnlinePeriod.EndAt != x.ServantDailyOnlinePeriod.StartAt)
        .Where(x => !(x.ServantDailyOnlinePeriod.StartAt.Hour >= 21 && x.ServantDailyOnlinePeriod.StartAt.Hour <= 3) &&
        !(x.ServantDailyOnlinePeriod.EndAt.Hour >= 21 && x.ServantDailyOnlinePeriod.EndAt.Hour <= 3)
        )
        .OrderByDescending(x => x.ServantDailyOnlinePeriod.StartAt)
        .GroupBy(x => x.ServantDailyStatistic.ServantId)
        .Select(x => x.ToList())
        .AsNoTracking()
        .ToListAsync();


        var response = dailyStats.Select(day =>
        {

            var hours = day.Select(x =>
            {

                // count all online time for each day except 00:00 - 06:00
                var totalSeconds = x.ServantDailyOnlinePeriod.EndAt.Subtract(x.ServantDailyOnlinePeriod.StartAt).TotalSeconds;
                return totalSeconds;

            }).ToList();


            return new ListServantsOnlineHistory
            (
                day.FirstOrDefault()?.ServantDailyStatistic.Servant.FirstName,
                day.FirstOrDefault()?.ServantDailyStatistic.Servant.LastName,
                day.FirstOrDefault()?.ServantDailyStatistic.ServantId ?? (ulong)0,
                TimeSpan.FromSeconds((double)(hours.Sum(x => x)))

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