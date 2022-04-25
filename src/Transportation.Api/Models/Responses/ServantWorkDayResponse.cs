namespace Tranportation.Api.Responses;

public class ServantWorkDayResponse
{
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
    public TimeSpan DiffInTime { get; set; }

}
public class ServantWorkDaysResponse
{
    public string? TotalTime { get; set; }
    public IEnumerable<ServantWorkDayResponse> Items { get; set; } = Array.Empty<ServantWorkDayResponse>();

}
