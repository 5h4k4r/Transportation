using System.ComponentModel.DataAnnotations;
using Core.Interfaces;
using Core.Constants;

namespace Core.Models.Common;

public class PaginatedRequest : IPagingOptions
{
    /// <summary>
    ///     The maximum number of document returned per page.
    /// </summary>
    /// <summary>
    ///     The default number of documents returned per page.
    /// </summary>
    [Range(0, CoreConstants.MaxPaginationLimit)]
    public int? Limit { get; set; } = CoreConstants.DefaultPaginationLimit;

    public int? Page { get; set; } = 0;
}