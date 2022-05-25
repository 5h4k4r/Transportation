namespace Core.Models.Repositories;


public class ServantOnlinePeriod
{

    public TimeSpan? TotalOnlineTime { get; set; }
    public DateTime? Date { get; set; }
    public IEnumerable<ServantOnlinePeriodItem> Periods { get; set; } = Array.Empty<ServantOnlinePeriodItem>();

}
public class ServantOnlinePeriods
{
    public double TotalTimeInSeconds { get; set; }
    public string? TotalTime { get; set; }
    public IEnumerable<ServantOnlinePeriod> Items { get; set; } = Array.Empty<ServantOnlinePeriod>();

}
public class ServantOnlinePeriodItem
{

    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
    public TimeSpan DiffInTime { get; set; }
    public double TotalPeriodInSeconds { get; set; }
}
