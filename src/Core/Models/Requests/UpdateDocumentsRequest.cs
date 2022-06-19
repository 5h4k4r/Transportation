using System.Text.Json.Serialization;

namespace Core.Models.Requests;

public class UpdateDocumentsRequest
{
    public string? Path { get; set; } = null!;

    [JsonIgnore] public string Type { get; set; } = null!;

    [JsonIgnore] public bool IsVerified { get; set; }

    [JsonIgnore] public string ModelType { get; set; } = null!;

    [JsonIgnore] public ulong ModelId { get; set; }

    [JsonIgnore] public DateTime? UpdatedAt { get; set; }
}