using System.Text.Json.Serialization;
using Core.Models.Repositories;

namespace Core.Models.Requests;

public class UpdateVehicleRequest
{
    public string Title { get; set; } = null!;
    public ulong? UsageId { get; set; }
    public IEnumerable<ulong> ServiceAreaTypes { get; set; } = null!;
    [JsonIgnore] public DateTime? CreatedAt { get; set; }
    [JsonIgnore] public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

    // public VehicleDetailDTO? VehicleDetail { get; set; }
    public IEnumerable<UpdateVehicleDetailRequest>? VehicleDetails { get; set; } = null!;
}

public class UpdateVehicleDetailRequest
{
    public string? Color { get; set; } = string.Empty;

    [JsonIgnore] public DateTime? CreatedAt { get; set; }

    public DateOnly? InsuranceExpire { get; set; }
    public string? InsuranceNo { get; set; }
    public string? Model { get; set; } = string.Empty;
    public PlaqueDtoResponse? Plaque { get; set; }
    public string? Tip { get; set; } = string.Empty;

    [JsonIgnore] public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

    public string? Vin { get; set; } = string.Empty;
}