using Core.Models;

namespace Infra.Repositories;


public class ServantWorkDay
{
    public DateTime? Date { get; set; }
    public IEnumerable<ServantWorkDayPeriodItem> Periods { get; set; } = Array.Empty<ServantWorkDayPeriodItem>();
    public TimeSpan TotalOnlineTimeInDay { get; set; }

}
public class ServantWorkDays
{
    public TimeSpan? TotalTime { get; set; }
    public IEnumerable<ServantWorkDay> Items { get; set; } = Array.Empty<ServantWorkDay>();

}
public class ServantWorkDayPeriodItem
{

    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
    public TimeSpan DiffInTime { get; set; }
    public double TotalDiffInSeconds { get; set; }
}
