using System.ComponentModel.DataAnnotations;

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
    public const int MaxPaginationLimit = 100;

    /// <summary>
    /// The default number of documents returned per page.
    /// </summary>
    public const int DefaultPaginationLimit = 20;


    [Range(0, MaxPaginationLimit)]
    int? Limit { get; set; }
}