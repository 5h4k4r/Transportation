using System.ComponentModel.DataAnnotations;

namespace Core.Models.Requests;


public class AuthCheckRequest : IValidatableObject
{

    [Required]
    public string? Mobile { get; set; }
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Mobile is null)
            yield return new ValidationResult($"The field {nameof(Mobile)} is required", new[] { nameof(Mobile) });
    }

}