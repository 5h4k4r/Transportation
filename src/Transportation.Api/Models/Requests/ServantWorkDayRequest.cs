using System.ComponentModel.DataAnnotations;

namespace Transportation.Api.Models.Requests
{
    public class ServantWorkDayRequest : IValidatableObject
    {
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public ulong? ServantId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartDate.HasValue && EndDate.HasValue && StartDate.Value > EndDate.Value)
            {
                yield return new ValidationResult("Start date must be before end date", new[] { nameof(StartDate) });
            }
        }
    }
}