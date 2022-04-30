using System.ComponentModel.DataAnnotations;
using Core;
using Core.Interfaces;

namespace Infra.Requests;


public class ListServantsOnlineHistoryRequest : IPagingOptions, IValidatableObject
{
    public DateTime StartDate { get; set; } = DateTime.Today;
    public DateTime EndDate { get; set; } = DateTime.Today.AddDays(1);
    public int? Page { get; set; } = 0;
    public bool WithDurationOnTask { get; set; } = false;

    [Range(0, Constants.MaxPaginationLimit)]
    public int? Limit { get; set; } = Constants.DefaultPaginationLimit;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {

        if (StartDate > EndDate)
        {
            yield return new ValidationResult($"The field {nameof(StartDate)} must be greater than {nameof(EndDate)}", new[] { nameof(StartDate) });
        }
    }
}