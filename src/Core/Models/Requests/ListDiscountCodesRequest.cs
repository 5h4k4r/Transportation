using System.ComponentModel.DataAnnotations;
using Core.Constants;
using Core.Interfaces;
using Core.Validations;

namespace Core.Models.Requests;

public class ListDiscountCodesRequest : IPagingOptions, ISortOptions, IValidatableObject
{
    /// <summary>
    ///     Allowed Values: "Code", "Type"
    /// </summary>
    [AllowedValues("Code", "Type")]
    public string? SearchField { get; set; }

    public string? SearchValue { get; set; }

    public bool? ActiveCodesOnly { get; set; } = false;
    public int? Page { get; set; }

    [Range(0, CoreConstants.MaxPaginationLimit)]
    public int? Limit { get; set; } = CoreConstants.DefaultPaginationLimit;

    /// <summary>
    ///     Allowed Values: "Id", "Code", "Value","AreaId","UsageLimit","Status","StartAt","ExpireAt"
    /// </summary>
    [AllowedValues("Id", "Code", "Value", "AreaId", "UsageLimit", "Status", "StartAt", "ExpireAt")]

    public string? SortField { get; set; }

    public bool? SortDescending { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (SearchField == null && SearchValue != null)
            yield return new ValidationResult($"The field {nameof(SearchField)} is required",
                new[] { nameof(SearchField) });

        else if (SearchField != null && SearchValue == null)
            yield return new ValidationResult($"The field {nameof(SearchValue)} is required",
                new[] { nameof(SearchValue) });
    }
}