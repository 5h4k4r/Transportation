using System;
using System.Collections.ObjectModel;
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
        .Where(x => !(x.ServantDailyOnlinePeriod.StartAt.Hour + 3 >= 0 && x.ServantDailyOnlinePeriod.StartAt.Hour + 3 <= 6) &&
        !(x.ServantDailyOnlinePeriod.EndAt.Hour + 3 >= 0 && x.ServantDailyOnlinePeriod.EndAt.Hour + 3 <= 6)
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

                var morning = new DateTime(WorkingHours.ServantDailyOnlinePeriod.StartAt.Year, WorkingHours.ServantDailyOnlinePeriod.StartAt.Month, WorkingHours.ServantDailyOnlinePeriod.StartAt.Day, ExcludeEndHour ?? 0, 0, 0, 0);
                var afternoon = new DateTime(WorkingHours.ServantDailyOnlinePeriod.StartAt.Year, WorkingHours.ServantDailyOnlinePeriod.StartAt.Month, WorkingHours.ServantDailyOnlinePeriod.StartAt.AddDays(1).Day, ExcludeStartHour ?? 0, 0, 0, 0);

                if (StartAt.Hour + 3 < 6)
                    StartAt = morning;

                if (EndAt.Hour + 3 > 0 && EndAt.Hour + 3 < 6)
                {
                    EndAt = afternoon;
                }

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
         .Where(x => (ExcludeStartHour == null && ExcludeEndHour == null) || !(x.ServantDailyOnlinePeriod.StartAt.Hour >= 0 && x.ServantDailyOnlinePeriod.StartAt.Hour <= 6) &&
         !(x.ServantDailyOnlinePeriod.EndAt.Hour >= 0 && x.ServantDailyOnlinePeriod.EndAt.Hour <= 6)
         )
         .OrderByDescending(x => x.ServantDailyOnlinePeriod.StartAt)
         .GroupBy(x => x.ServantDailyStatistic.DayId)
         .AsNoTracking()
         .CountAsync();


        return count;
    }
    public async Task<List<ListServantsOnlineHistory>?> ListServantsOnlineHistory(ListServantsOnlineHistoryRequest model)
    {
        var startDate = model.StartDate.StartOfDay();
        var endDate = model.EndDate.EndOfDay();


        var servants = await _context.ServantDailyStatistics
        .Where(x => x.CreatedAt >= startDate && x.CreatedAt <= endDate)
        .Include(x => x.Servant)
        .GroupBy(x => x.ServantId)
        .Select(x => new { x.Key, Hours = (double)(x.Sum(y => y.OnlineDuration) - (model.WithDurationOnTask ? x.Sum(y => y.DurationOnTask) : 0)) / (double)3600, x.FirstOrDefault().Servant })
        .OrderByDescending(x => x.Hours)
        .ToListAsync();


        var response = servants.Select(x =>
         {
             TimeSpan OnlineHours = TimeSpan.FromSeconds((double)x.Hours);


             return new ListServantsOnlineHistory(x.Servant.FirstName, x.Servant.LastName, x.Servant.UserId, x.Hours);
         }).ToList();


        return response;
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