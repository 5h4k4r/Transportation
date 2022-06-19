using System.Text.Json.Serialization;

namespace Core.Models.Requests;

public class CreateServantRequest
{
    public ulong UserId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string NationalId { get; set; } = null!;
    public string? Certificate { get; set; }
    public string? BankId { get; set; }
    [JsonIgnore] public DateTime? UpdateAt { get; set; } = DateTime.UtcNow;
    public uint AreaId { get; set; }
    public byte? GenderId { get; set; }
    public string Address { get; set; } = null!;


    public List<UpdateDocumentsRequest>? Documents { get; set; }
}