using System.ComponentModel.DataAnnotations;
using Core.Constants;
using Core.Interfaces;
using Core.Models.Common;
using Core.Validations;

namespace Core.Models.Requests;

public class ListTasksByClientRequest : IPagingOptions, ISortOptions, IValidatableObject
{
    private string? _sortField;
    public ulong? ServantId { get; set; }
    public DateTime? TaskCreatedFrom { get; set; }
    public DateTime? TaskCreatedTo { get; set; }
    public TaskState? Status { get; set; } = null;
    public bool? IncludeRequest { get; set; }
    public bool? IncludeServant { get; set; }
    public int? Page { get; set; } = 0;
    public int? Limit { get; set; } = CoreConstants.DefaultPaginationLimit;

    /// <summary>
    ///     Allowed Values: "Client.Id", "Task.Id", "Task.Price", "Task.ServantId", "Task.CreatedAt", "Task.UpdatedAt",
    ///     "Client.CreatedAt", "Client.UpdatedAt"
    /// </summary>
    [AllowedValues(
        "Member.Id", "Task.Id",
        "Task.Price", "Task.ServantId",
        "Task.CreatedAt", "Task.UpdatedAt",
        "Member.CreatedAt", "Member.UpdatedAt"
    )]
    public string? SortField
    {
        get => _sortField;
        set
        {
            // if value contains 'Client' then change it to Member to match the database
            if (value != null && value.Contains("Client"))
                _sortField = value.Replace("Client", "Member");
            else
                _sortField = value;
        }
    }

    public bool? SortDescending { get; set; } = false;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        // if (ClientId is null)
        //     yield return new ValidationResult($"The field {nameof(ClientId)} is required", new[] { nameof(ClientId) });


        if (TaskCreatedFrom > TaskCreatedTo)
            yield return new ValidationResult(
                $"The field {nameof(TaskCreatedFrom)} must be greater than {nameof(TaskCreatedTo)}",
                new[] { nameof(TaskCreatedFrom) });
    }
}