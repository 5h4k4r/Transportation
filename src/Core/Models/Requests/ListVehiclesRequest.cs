using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Core.Interfaces;

namespace Core.Requests;

public class ListVehiclesRequest : IPagingOptions, IValidatableObject
{
    public int? Page { get; set; }
    [Range(0, Constants.MaxPaginationLimit)]
    public int? Limit { get; set; }
    /// <summary>
    /// Allowed Values: id, title, plateNumber, vim
    /// </summary>
    public ListVehicleRequestFilterField? FilterField { get; set; }
    public string? FilterValue { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!FilterField.HasValue && FilterValue != null)
            yield return new ValidationResult($"The field {nameof(FilterValue)} is required", new[] { nameof(FilterValue) });

        else if (FilterField.HasValue && FilterValue == null)
            yield return new ValidationResult($"The field {nameof(FilterField)} is required", new[] { nameof(FilterField) });

    }
}

public enum ListVehicleRequestFilterField
{
    Id = 0,
    Title = 1,
    PlateNumber = 2,
    Vin = 3
}