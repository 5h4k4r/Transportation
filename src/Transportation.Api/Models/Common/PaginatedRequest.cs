using System.ComponentModel.DataAnnotations;
using Tranportation.Api;
using Transportation.Api.Interfaces;

namespace Transportation.Api.Common;
public class PaginatedRequest : IPagingOptions
{
    /// <summary>
    /// The maximum number of document returned per page.
    /// </summary>

    /// <summary>
    /// The default number of documents returned per page.
    /// </summary>
    [Range(0, Constants.MaxPaginationLimit)]
    public int? Limit { get; set; } = Constants.DefaultPaginationLimit;
    public int? Page { get; set; } = 0;
}