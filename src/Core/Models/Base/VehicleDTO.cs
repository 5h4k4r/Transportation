namespace Core.Models.Base;

public class VehicleDto
{
    public ulong Id { get; set; }
    public string Title { get; set; }
    public ulong? UsageId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    // public VehicleDetailDTO? VehicleDetail { get; set; }
    public virtual ICollection<VehicleDetailDto>? VehicleDetails { get; set; }
}