using System.ComponentModel.DataAnnotations;
using Core.Interfaces;

namespace Core.Common;
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