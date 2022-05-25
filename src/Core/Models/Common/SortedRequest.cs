using Core.Interfaces;

namespace Core.Models.Common;

public class SortedRequest : ISortOptions
{
    public string? SortField { get; set; } = "Id";
    public bool? SortDescending { get; set; } = false;
}