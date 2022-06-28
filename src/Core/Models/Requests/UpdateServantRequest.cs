using System.Text.Json.Serialization;

namespace Core.Models.Requests;

public class UpdateServantRequest
{
    public string? FirstName { get; set; } = null!;
    public string? LastName { get; set; } = null!;
    public string? NationalId { get; set; } = null!;
    public string? Certificate { get; set; }
    public string? BankId { get; set; }
    public uint? AreaId { get; set; }
    public byte? GenderId { get; set; }
    public string? Address { get; set; } = null!;
    [JsonIgnore] public DateTime? UpdateAt { get; set; } = DateTime.UtcNow;
    public List<KeyValuePair<string,string>>? Documents { get; set; }
}