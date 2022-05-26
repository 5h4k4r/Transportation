using System.ComponentModel.DataAnnotations;
using Core.Interfaces;

namespace Core.Models.Requests;


public class ListServantsOnlineHistoryRequest : IPagingOptions, IValidatableObject
{
    public DateTime StartDate { get; set; } = DateTime.Today;
    public DateTime EndDate { get; set; } = DateTime.Today.AddDays(1);
    [Range(0, 24)]
    public int? ExcludeStartHour { get; set; }
    [Range(0, 24)]
    public int? ExcludeEndHour { get; set; }

    public int? Page { get; set; } = 0;

    [Range(0, CoreConstants.MaxPaginationLimit)]
    public int? Limit { get; set; } = CoreConstants.DefaultPaginationLimit;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {

        if (StartDate > EndDate)
        {
            yield return new ValidationResult($"The field {nameof(StartDate)} must be greater than {nameof(EndDate)}", new[] { nameof(StartDate) });
        }
        if ((ExcludeStartHour is null && ExcludeEndHour is not null) || (ExcludeStartHour is not null && ExcludeEndHour is null))
        {
            if (ExcludeStartHour is null)
                yield return new ValidationResult($"The field {nameof(ExcludeStartHour)} is required", new[] { nameof(ExcludeStartHour) });

            if (ExcludeEndHour is null)
                yield return new ValidationResult($"The field {nameof(ExcludeEndHour)} is required", new[] { nameof(ExcludeEndHour) });

        }
    }
}