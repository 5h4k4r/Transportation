using System.ComponentModel.DataAnnotations;

namespace Transportation.Api.Requests;

public class LoginRequest
{
    [Required]
    public string AuthId { get; set; } = string.Empty;
    [Required]
    public string AuthToken { get; set; } = string.Empty;
    [Required]
    public string Mobile { get; set; } = string.Empty;
}