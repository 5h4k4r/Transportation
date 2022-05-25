using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Core.Validations;

namespace Core.Models.Requests;


public class CreateLanguageRequest
{
    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Locale { get; set; } = string.Empty;

    /// <summary>
    /// Allowed values: "ltr" or "rtl"
    /// </summary>
    [Required]
    [AllowedValues("ltr", "rtl")]
    public string Direction { get; set; } = string.Empty;
    [JsonIgnore]
    public DateTime? CreatedAt { get; set; } = DateTime.Today;
    [JsonIgnore]
    public DateTime? UpdatedAt { get; set; } = DateTime.Today;


}