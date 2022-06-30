using System.Text.Json.Serialization;
using Core.Interfaces;

namespace Core.Models.Requests;

public class ListFilesRequest : IPagingOptions, ISortOptions
{
    public FilterFileType SearchValue { get; set; } = FilterFileType.All;

    public int? Page { get; set; }
    public int? Limit { get; set; }

    [JsonIgnore] public string? SortField { get; set; } = "CreatedAt";
    public bool? SortDescending { get; set; } = true;
}

public enum FilterFileType

{
    All = 0,
    Service = 1,
    ServiceAreaType = 2
}