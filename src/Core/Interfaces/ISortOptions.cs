namespace Core.Interfaces;

public interface ISortOptions
{
    /// <summary>
    ///     The field to sort by.
    /// </summary>
    public string? SortField { get; set; }

    /// <summary>
    ///     Apply descending sort.
    /// </summary>
    public bool? SortDescending { get; set; }

    bool IsEmpty()
    {
        return string.IsNullOrEmpty(SortField);
    }
}