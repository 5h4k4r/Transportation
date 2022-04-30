using System.ComponentModel.DataAnnotations;
using Core;
using Core.Common;
using Core.Interfaces;

namespace Infra.Requests;



public class ListTasksRequest : IPagingOptions, ISortOptions, IValidatableObject
{
    [Required]
    public ulong? AreaId { get; set; }
    public DateTime StartAt { get; set; } = DateTime.Today;


    public DateTime EndAt { get; set; } = DateTime.Today;
    public TaskState? Status { get; set; } = null;
    /// <summary>
    /// Allowed Values: Task.Id, Task.Price, Task.Tip, Task.Status, Task.CreatedAt, Task.UpdatedAt, Task.RequestId, Destination.Distance, Destination.Duration
    /// </summary>
    [AllowedValues(
        "Task.Id", "Task.Price",
        "Task.Tip", "Task.Status",
        "Task.CreatedAt", "Task.UpdatedAt",
        "Task.RequestId", "Destination.Distance",
        "Destination.Duration"
    )]
    public string? SortField { get; set; }
    public bool? SortDescending { get; set; } = false;
    public int? Page { get; set; } = 0;
    public int? Limit { get; set; } = Constants.DefaultPaginationLimit;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (AreaId is null)
            yield return new ValidationResult($"The field {nameof(AreaId)} is required", new[] { nameof(AreaId) });


        if (StartAt > EndAt)
        {
            yield return new ValidationResult($"The field {nameof(StartAt)} must be greater than {nameof(EndAt)}", new[] { nameof(StartAt) });
        }
    }

}

