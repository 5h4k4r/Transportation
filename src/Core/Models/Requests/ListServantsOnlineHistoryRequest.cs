using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Core.Models.Requests;

public class ListServantsOnlineHistoryRequest : IValidatableObject
{
    public uint? ServantId { get; set; } = null;
    public DateTime StartDate { get; set; } = DateTime.Today;
    public DateTime EndDate { get; set; } = DateTime.Today.AddDays(1);

    [Range(0, 24)] public int? ExcludeStartHour { get; set; }

    [Range(0, 24)] public int? ExcludeEndHour { get; set; }

    [JsonIgnore] public ulong? AreaId { get; set; }
    public double MinHours { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (StartDate > EndDate)
            yield return new ValidationResult($"The field {nameof(StartDate)} must be greater than {nameof(EndDate)}",
                new[] { nameof(StartDate) });

        if ((ExcludeStartHour is not null || ExcludeEndHour is null) &&
            (ExcludeStartHour is null || ExcludeEndHour is not null)) yield break;

        if (ExcludeStartHour is null)
            yield return new ValidationResult($"The field {nameof(ExcludeStartHour)} is required",
                new[] { nameof(ExcludeStartHour) });

        if (ExcludeEndHour is null)
            yield return new ValidationResult($"The field {nameof(ExcludeEndHour)} is required",
                new[] { nameof(ExcludeEndHour) });
    }
}