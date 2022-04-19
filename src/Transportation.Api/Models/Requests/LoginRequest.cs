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

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (AuthId is null)
            yield return new ValidationResult($"The field {nameof(AuthId)} is required", new[] { nameof(AuthId) });
        if (AuthToken is null)
            yield return new ValidationResult($"The field {nameof(AuthToken)} is required", new[] { nameof(AuthToken) });
        if (Mobile is null)
            yield return new ValidationResult($"The field {nameof(Mobile)} is required", new[] { nameof(Mobile) });
    }

}