using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Transportation.Api.Requests;
public class ServantPerformanceRequest : IValidatableObject
{
    [Required]
    [JsonIgnore]
    public ulong? UserId { get; set; }
    public DateTime? EndAt { get; set; } = DateTime.Today;
    public DateTime? StartAt { get; set; } = DateTime.Today;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (UserId is null)
            yield return new ValidationResult($"The field {nameof(UserId)} is required", new[] { nameof(UserId) });
        if (EndAt > StartAt)
            yield return new ValidationResult($"The field {nameof(EndAt)} must be greater than {nameof(StartAt)}", new[] { nameof(EndAt), nameof(StartAt) });
    }
}
