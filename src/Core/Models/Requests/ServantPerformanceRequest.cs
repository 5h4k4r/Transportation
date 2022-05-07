using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Infra.Requests;

public class ServantPerformanceRequest : IValidatableObject
{
    public DateTime? StartAt { get; set; } = DateTime.Today;
    public DateTime? EndAt { get; set; } = DateTime.Today;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (StartAt > EndAt)
            yield return new ValidationResult($"The field {nameof(EndAt)} must be greater than {nameof(StartAt)}", new[] { nameof(EndAt), nameof(StartAt) });
    }
}
