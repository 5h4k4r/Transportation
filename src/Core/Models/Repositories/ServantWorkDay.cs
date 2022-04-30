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
    public string? TotalTime { get; set; }
    public List<ServantWorkDayDTO> Items { get; set; } = new List<ServantWorkDayDTO>();

}
public class ServantWorkDayPeriodItem
{

    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
    public TimeSpan DiffInTime { get; set; }
    public double TotalDiffInSeconds { get; set; }
}
