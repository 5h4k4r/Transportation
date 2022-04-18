using Transportation.Api.Interfaces;

namespace Tranportation.Api.Common;

public class SortedRequest : ISortOptions
{
    public string? SortField { get; set; } = "Id";
    public bool? SortDescending { get; set; } = false;
}