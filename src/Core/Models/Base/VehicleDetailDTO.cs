namespace Core.Models.Base;

public class VehicleDetailDto
{
    public ulong? Id { get; set; }
    public ulong VehicleId { get; set; }
    public string? Plaque { get; set; }
    public string? Color { get; set; }
    public string? Tip { get; set; }
    public string? Model { get; set; }
    public string? InsuranceNo { get; set; }
    public DateOnly? InsuranceExpire { get; set; }
    public string? Vin { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public VehicleDto? Vehicle { get; set; }
}