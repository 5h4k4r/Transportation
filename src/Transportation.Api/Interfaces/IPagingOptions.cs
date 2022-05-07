using System.ComponentModel.DataAnnotations;
using Tranportation.Api;

namespace Transportation.Api.Interfaces;

public interface IPagingOptions
{
    /// <summary>
    /// Gets or sets the page index of the current pagination.
    /// </summary>
    int? Page { get; set; }

    /// <summary>
    /// Gets or sets the total items per page limit.
    /// </summary>
    /// <summary>
    /// The default number of documents returned per page.
    /// </summary>

    [Range(0, Constants.MaxPaginationLimit)]
    int? Limit { get; set; }
}