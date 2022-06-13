using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Core.Requests;


public class CreatedDepartmentRequest
{
    [Required]
    public string Title { get; set; } = string.Empty;


    // [JsonIgnore]
    public DateTime? CreatedAt { get; set; } = DateTime.Today;
    // [JsonIgnore]
    public DateTime? UpdatedAt { get; set; } = DateTime.Today;


}