namespace Core.Models.Requests;

public class CreateDiscountRequest
{
    public uint ServiceAreaTypeId { get; set; }
    public double Value { get; set; }
    public ushort Max { get; set; }
    public byte? Limit { get; set; }
    public TimeOnly StartAt { get; set; }
    public TimeOnly EndAt { get; set; }
}