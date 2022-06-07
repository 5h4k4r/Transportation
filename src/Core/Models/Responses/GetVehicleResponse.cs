namespace Core.Models.Repositories;

public class VehicleDetailDtoResponse
{
    public ulong? Id { get; set; }
    public ulong? VehicleId { get; set; }
    public PlaqueDtoResponse? Plaque { get; set; }
    public string? Color { get; set; }
    public string? Tip { get; set; }
    public string? Model { get; set; }
    public string? InsuranceNo { get; set; }
    public DateOnly? InsuranceExpire { get; set; }
    public string? Vin { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public VehicleDtoResponse? Vehicle { get; set; }
}

public class PlaqueDtoResponse
{
    public string? City { get; set; }
    public string? Code { get; set; }
    public string? Text { get; set; }
    public string? Color { get; set; }
    public string? Country { get; set; }
    public string? Textr { get; set; }
}

public class VehicleDtoResponse
{
    public ulong Id { get; set; }
    public string Title { get; set; } = null!;
    public ulong? UsageId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public ICollection<ServiceResponse> Services { get; set; } = null!;

    public virtual VehicleDetailDtoResponse VehicleDetails { get; set; }
}

public class ServiceResponse
{
    public ulong Id { get; set; }
    public string Title { get; set; } = null!;
}