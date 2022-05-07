using Core.Interfaces;

namespace Core.Common;

public class SortedRequest : ISortOptions
{
    public string? SortField { get; set; } = "Id";
    public bool? SortDescending { get; set; } = false;
}