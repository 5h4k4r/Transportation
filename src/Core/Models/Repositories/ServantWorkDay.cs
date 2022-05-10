using Core.Models;

namespace Infra.Repositories;


public class ServantOnlinePeriod
{
    public TimeSpan TotalOnlineTimeInDay { get; set; }
    public DateTime? Date { get; set; }
    public IEnumerable<ServantOnlinePeriodItem> Periods { get; set; } = Array.Empty<ServantOnlinePeriodItem>();

}
public class ServantOnlinePeriods
{
    public TimeSpan? TotalTime { get; set; }
    public IEnumerable<ServantOnlinePeriod> Items { get; set; } = Array.Empty<ServantOnlinePeriod>();

}
public class ServantOnlinePeriodItem
{

    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
    public TimeSpan DiffInTime { get; set; }
    public double TotalDiffInSeconds { get; set; }
}
