namespace Core.Models.Repositories;


public class DiscountCodeDetail
{
    public ulong Id { get; set; }
    public string Code { get; set; } = null!;
    public double Value { get; set; }
    public string Type { get; set; } = null!;
    public Detail? Detail { get; set; } = null!;
    public ulong AreaId { get; set; }
    public ushort? UsageLimit { get; set; }
    public byte Status { get; set; }
    public DateTime StartAt { get; set; }
    public DateTime? ExpireAt { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}

public class Detail
{
    public ulong Steps { get; set; }
    
    public ulong Max { get; set; }
    
    public double Value { get; set; }
    
}