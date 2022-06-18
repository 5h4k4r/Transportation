using Core.Models.Base;

namespace Core.Models.Responses;

public class ListServicesResponses
{
    public ulong Id { get; set; }
    public string Pin { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public ICollection<ServiceAreaTypeDtoResponse?> ServiceAreaTypes { get; set; } = null!;
}

public class ServiceAreaTypeDtoResponse
{
    public ulong Id { get; set; }
    public ulong ServiceId { get; set; }
    public string AreaId { get; set; } = null!;
    public ulong CategoryId { get; set; }
    public ulong? TypeId { get; set; }
    public ulong? UsageId { get; set; }
    public string Currency { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public CategoryDto? Category { get; set; }
    public ServiceDto? Service { get; set; }
}