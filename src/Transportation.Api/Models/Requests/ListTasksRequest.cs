using System.ComponentModel.DataAnnotations;
using Tranportation.Api.Common;
using Transportation.Api.Common;
using Transportation.Api.Interfaces;

namespace Tranportation.Api.Requests;


public class ListTasksRequest : IPagingOptions, ISortOptions
{
    [Required]
    public ulong AreaId { get; set; }
    public DateTime? EndAt { get; set; } = null;
    public DateTime? StartAt { get; set; } = null;
    public TaskState? TaskState { get; set; }
    public string? SortField { get; set; }
    public bool? SortDescending { get; set; } = false;
    public int? Page { get; set; } = 0;
    public int? Limit { get; set; } = Constants.DefaultPaginationLimit;

}