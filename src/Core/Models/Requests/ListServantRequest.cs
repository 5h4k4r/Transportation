using System.ComponentModel.DataAnnotations;
using Core.Interfaces;

namespace Core.Requests;

public class ListServantRequest : IPagingOptions, ISortOptions, IValidatableObject
{
    /// <summary>
    /// Allowed Values: "Name", "NationalId", "PhoneNumber"
    /// </summary>
    [AllowedValues("Name", "NationalId", "PhoneNumber")]
    public string? SearchField { get; set; }
    public string? SearchValue { get; set; }
    public int? Page { get; set; } = 0;
    [Range(0, Constants.MaxPaginationLimit)]
    public int? Limit { get; set; } = Constants.DefaultPaginationLimit;
    /// <summary>
    /// Allowed Values:  "Address", "AreaId", "CreatedAt", "Id", "UserId", "BankId", "Certificate", "NationalId", "FirstName", "LastName", "GenderId" 
    /// </summary>
    [AllowedValues("Address", "AreaId", "CreatedAt", "Id", "UserId", "BankId", "Certificate", "NationalId", "FirstName", "LastName", "GenderId")]
    public string? SortField { get; set; }
    public bool? SortDescending { get; set; } = false;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (SearchField == null && SearchValue != null)
            yield return new ValidationResult($"The field {nameof(SearchField)} is required", new[] { nameof(SearchField) });

        else if (SearchField != null && SearchValue == null)
            yield return new ValidationResult($"The field {nameof(SearchValue)} is required", new[] { nameof(SearchValue) });
    }
}