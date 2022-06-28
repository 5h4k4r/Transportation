namespace Core.Models.Requests;

public class UpdateServiceAreaTypeRequest
{
    public ulong? Icon { get; set; }
    public double? BasePrice { get; set; }
    public double? BaseTime { get; set; }
    public double? BaseStop { get; set; }
    public double? BaseStopDistance { get; set; }
    public double? BaseDistance { get; set; }
    public double? MinPrice { get; set; }
    public double? Tip { get; set; }
    public double? MinTip { get; set; }
    public double? MaxTip { get; set; }
    public double? BaseNight { get; set; }
    public double? BaseNightStart { get; set; }
    public double? BaseNightEnd { get; set; }
}