using System.ComponentModel.DataAnnotations;
using Transportation.Api.Common;
using Transportation.Api.Interfaces;

namespace Tranportation.Api.Requests;


public class ListTasksByClientRequest : IPagingOptions, ISortOptions, IValidatableObject
{
    private string? _SortField;

    [Required]
    public ulong? ClientId { get; set; }
    public DateTime? TaskCreatedFrom { get; set; }
    public DateTime? TaskCreatedTo { get; set; }
    public TaskState? Status { get; set; } = null;
    public bool? IncludeRequest { get; set; }
    public bool? IncludeServant { get; set; }

    /// <summary>
    /// Allowed Values: "Client.Id", "Task.Id", "Task.Price", "Task.ServantId", "Task.CreatedAt", "Task.UpdatedAt", "Client.CreatedAt", "Client.UpdatedAt"
    /// </summary>
    [AllowedValues(
        "Member.Id", "Task.Id",
        "Task.Price", "Task.ServantId",
        "Task.CreatedAt", "Task.UpdatedAt",
        "Member.CreatedAt", "Member.UpdatedAt"
    )]
    public string? SortField
    {
        get
        {
            return _SortField;
        }
        set
        {
            // if value contains 'Client' then change it to Member to match the database
            if (value != null && value.Contains("Client"))
            {
                _SortField = value.Replace("Client", "Member");
            }
            else
            {
                _SortField = value;
            }
        }
    }

    public bool? SortDescending { get; set; } = false;
    public int? Page { get; set; } = 0;
    public int? Limit { get; set; } = Constants.DefaultPaginationLimit;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (ClientId is null)
            yield return new ValidationResult($"The field {nameof(ClientId)} is required", new[] { nameof(ClientId) });


        if (TaskCreatedFrom > TaskCreatedTo)
        {
            yield return new ValidationResult($"The field {nameof(TaskCreatedFrom)} must be greater than {nameof(TaskCreatedTo)}", new[] { nameof(TaskCreatedFrom) });
        }
    }

}

