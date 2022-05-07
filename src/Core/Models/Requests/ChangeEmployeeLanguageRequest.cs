using System.ComponentModel.DataAnnotations;

namespace Infra.Requests;


public class ChangeEmployeeLanguageRequest : IValidatableObject
{

    public ulong UserId { get; set; }

    public int? LanguageId { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (LanguageId is null)
            yield return new ValidationResult($"The field {nameof(LanguageId)} is required", new[] { nameof(LanguageId) });

    }
}