using System.ComponentModel.DataAnnotations;
using Core.Constants;
using Core.Interfaces;
using Core.Validations;

namespace Core.Models.Requests;

public class ListServantRequest : IPagingOptions, ISortOptions, IValidatableObject
{
    /// <summary>
    ///     Allowed Values: "Name", "NationalId", "PhoneNumber"
    /// </summary>
    [AllowedValues("Name", "NationalId", "PhoneNumber", "Id")]
    public string? SearchField { get; set; }

    public ulong? AreaId { get; set; }
    public JobStatus.ServantStatus? Status { get; set; }
    public string? SearchValue { get; set; }
    public bool IncompleteOnly { get; set; } = false;
    public int? Page { get; set; } = 0;

    [Range(0, CoreConstants.MaxPaginationLimit)]
    public int? Limit { get; set; } = CoreConstants.DefaultPaginationLimit;

    /// <summary>
    ///     Allowed Values:  "Address", "AreaId", "CreatedAt", "Id", "UserId", "BankId", "Certificate", "NationalId",
    ///     "FirstName", "LastName", "GenderId"
    /// </summary>
    [AllowedValues("Address", "AreaId", "CreatedAt", "Id", "UserId", "BankId", "Certificate", "NationalId", "FirstName",
        "LastName", "GenderId")]
    public string? SortField { get; set; }

    public bool? SortDescending { get; set; } = false;

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