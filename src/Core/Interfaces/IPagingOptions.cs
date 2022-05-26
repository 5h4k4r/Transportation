using System.ComponentModel.DataAnnotations;


namespace Core.Interfaces;

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

    [Range(0, CoreConstants.MaxPaginationLimit)]
    int? Limit { get; set; }
}