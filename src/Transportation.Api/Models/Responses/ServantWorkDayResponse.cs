namespace Tranportation.Api.Responses;

public class ServantWorkDayResponse
{
    public DateTime Date { get; set; }
    public IEnumerable<ServantWorkDayPeriodItem> Periods { get; set; } = Array.Empty<ServantWorkDayPeriodItem>();
    public TimeSpan TotalOnlineTimeInDay { get; set; }

}
public class ServantWorkDaysResponse
{
    public string? TotalTime { get; set; }
    public List<ServantWorkDayResponse> Items { get; set; } = new List<ServantWorkDayResponse>();

}
public class ServantWorkDayPeriodItem
{

    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
    public TimeSpan DiffInTime { get; set; }
    public double TotalDiffInSeconds { get; set; }
}
