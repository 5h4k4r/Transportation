using Core.Interfaces;

namespace Core.Models.Common;

/// <summary>
/// Represents any paginated data/
/// </summary>
public class PaginatedResponse<T>
{
    /// <summary>
    /// Gets the remaining items in the paginated data.
    /// </summary>
    public long RemainingItems { get; }

    /// <summary>
    /// Gets the total count of the items in the data.
    /// </summary>
    /// <value></value>
    public long Count { get; }

    /// <summary>
    /// Gets the page index of the current pagination.
    /// </summary>
    public int Page { get; }

    /// <summary>
    /// Gets the total items per page limit.
    /// </summary>
    public int Limit { get; }

    /// <summary>
    /// Gets the metadata.
    /// </summary>
    public object? Metadata { get; }

    /// <summary>
    /// Gets the items in the current page.
    /// </summary>
    public ICollection<T> Items { get; }


    /// <summary>
    /// Creates a new instance of PaginatedResponse.
    /// </summary>
    public PaginatedResponse(in int count, in IPagingOptions pagingOptions, in ICollection<T> items)
    {
        Count = count;
        Page = pagingOptions.Page ?? 0;
        Limit = pagingOptions.Limit ?? 0;
        Items = items;
        RemainingItems = Math.Max(0, Count - (Page * Limit) - Items.Count);
    }

    /// <summary>
    /// Creates a new instance of PaginatedResponse.
    /// </summary>
    public PaginatedResponse(in int count, in int page, in int limit, in ICollection<T> items)
    {
        Count = count;
        Page = page;
        Limit = limit;
        Items = items;
        RemainingItems = Math.Max(0, Count - (Page * Limit) - Items.Count);
    }
    public PaginatedResponse(in int count, in IPagingOptions pagingOptions, in ICollection<T> items, object metadata)
    {
        Count = count;
        Page = pagingOptions.Page ?? 0;
        Limit = pagingOptions.Limit ?? 0;
        Metadata = metadata;
        Items = items;
        RemainingItems = Math.Max(0, Count - (Page * Limit) - Items.Count);
    }
}
