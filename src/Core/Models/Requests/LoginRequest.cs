using System.ComponentModel.DataAnnotations;

namespace Core.Models.Requests;


public class LoginRequest
{
    [Required]
    public string? AuthId { get; set; }
    [Required]
    public string? AuthToken { get; set; } 
    [Required]
    public string? Mobile { get; set; } 

    public IEnumerable<ValidationResult> Validate()
    {
        if (AuthId is null)
            yield return new ValidationResult($"The field {nameof(AuthId)} is required", new[] { nameof(AuthId) });
        if (AuthToken is null)
            yield return new ValidationResult($"The field {nameof(AuthToken)} is required", new[] { nameof(AuthToken) });
        if (Mobile is null)
            yield return new ValidationResult($"The field {nameof(Mobile)} is required", new[] { nameof(Mobile) });
    }

}