using System;
using Microsoft.EntityFrameworkCore;
using Tranportation.Api.Requests;
using Tranportation.Api.Responses;
using Transportation.Api.Interfaces;
using Transportation.Api.Model;
namespace Transportation.Api.Repositories;

public class ServantWorkDaysRepository : IServantWorkDaysRepository
{

    private readonly transportationContext _context;

    public ServantWorkDaysRepository(transportationContext repositoryContext) => _context = repositoryContext;

    public async Task<ServantWorkDaysResponse> GetServantWorkDays(ulong ServantId, ServantOnlinePeriodRequest model)
    {


        var DailyStats = await _context.ServantDailyStatistics
        .Where(x => x.ServantId == ServantId)
        .Join(
            _context.ServantDailyOnlinePeriods,
            ServantDailyStatistics => ServantDailyStatistics.Id,
            ServantDailyOnlinePeriods => ServantDailyOnlinePeriods.ServantDailyStatisticId,
            (ServantDailyStatistics, ServantDailyOnlinePeriods) => new
            {
                ServantDailyOnlinePeriods
            }
        )

        .Where(x => x.ServantDailyOnlinePeriods.StartAt <= model.EndDate)
        .Where(x => x.ServantDailyOnlinePeriods.StartAt >= model.StartDate)
        .Where(x => x.ServantDailyOnlinePeriods.EndAt <= model.EndDate)
        .Where(x => x.ServantDailyOnlinePeriods.EndAt >= model.StartDate)
        .Where(x => x.ServantDailyOnlinePeriods.EndAt != x.ServantDailyOnlinePeriods.StartAt)
        .Where(x =>
        !(x.ServantDailyOnlinePeriods.StartAt.Hour >= 0 && x.ServantDailyOnlinePeriods.StartAt.Hour <= 6) &&
        !(x.ServantDailyOnlinePeriods.EndAt.Hour >= 0 && x.ServantDailyOnlinePeriods.EndAt.Hour <= 6)
        )
        .OrderByDescending(x => x.ServantDailyOnlinePeriods.StartAt)
        .Select(x => new { x.ServantDailyOnlinePeriods.StartAt, x.ServantDailyOnlinePeriods.EndAt })
        .ToListAsync();


        var ServantWorkDays = DailyStats.Select(x =>
        {
            var StartAt = x.StartAt;
            var EndAt = x.EndAt;

            var morning = new DateTime(x.StartAt.Year, x.StartAt.Month, x.StartAt.Day, 6, 0, 0, 0);
            var afternoon = new DateTime(x.StartAt.Year, x.StartAt.Month, x.StartAt.AddDays(1).Day, 0, 0, 0, 0);

            if (StartAt.Hour < 6)
                StartAt = morning;

            if (EndAt.Hour > 0 && EndAt.Hour < 6)
            {
                EndAt = afternoon;
            }

            var Difference = EndAt.Subtract(StartAt);
            var TotalDiffInSeconds = Difference.TotalSeconds;
            TimeSpan DiffInTime = TimeSpan.FromSeconds((double)TotalDiffInSeconds);




            return new ServantWorkDayResponse
            {
                StartAt = StartAt,
                EndAt = EndAt,
                DiffInTime = DiffInTime
            };

        });

        var totalSeconds = ServantWorkDays.Sum(x => x.DiffInTime.TotalSeconds);


        TimeSpan time = TimeSpan.FromSeconds((double)totalSeconds);

        //here backslash is must to tell that colon is
        //not the part of format, it just a character that we want in output
        string totalTimeInString = time.ToString(@"hh\:mm\:ss\:fff");

        return new ServantWorkDaysResponse
        {
            TotalTime = totalTimeInString,
            Items = ServantWorkDays
        };


    }

    public async Task<List<ListServantsOnlineHistoryResponse>> ListServantsOnlineHistory(ServantOnlinePeriodRequest model)
    {

        var servants = await _context.ServantDailyStatistics
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
        .ToListAsync();

        var response = servants.Select(x =>
        {
            var OnlineDurations = servants
            .Where(s => s.Servants.Id == x.Servants.Id)
            .GroupBy(servant => servant.Servants.Id)
            .FirstOrDefault()?.Sum(d => d.ServantDailyStatistics.OnlineDuration) ?? 0;


            TimeSpan OnlineHours = TimeSpan.FromSeconds((double)OnlineDurations);

            //here backslash is must to tell that colon is
            //not the part of format, it just a character that we want in output
            string str = OnlineHours.ToString(@"hh\:mm\:ss\:fff");

            return new ListServantsOnlineHistoryResponse(x.Servants.FirstName, x.Servants.LastName, x.Servants.Id, OnlineHours);

        }

        ).OrderByDescending(x => x.OnlineHours).ToList();

        return response;
    }
}