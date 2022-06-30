namespace Core.Models.Base;

public class DiscountDto
{
    public ulong Id { get; set; }
    public ulong ServiceAreaTypeId { get; set; }
    public double Value { get; set; }
    public ushort Max { get; set; }
    public byte? Limit { get; set; }
    public string Periods { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}