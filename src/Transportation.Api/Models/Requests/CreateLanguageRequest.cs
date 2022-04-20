using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Tranportation.Api.Requests;

public class CreateLanguageRequest
{
    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Locale { get; set; } = string.Empty;

    [Required]
    public string Direction { get; set; } = string.Empty;
    [JsonIgnore]
    public DateTime? CreatedAt { get; set; } = DateTime.Today;
    [JsonIgnore]
    public DateTime? UpdatedAt { get; set; } = DateTime.Today;


}