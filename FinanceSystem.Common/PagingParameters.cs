namespace FinanceSystem.Common;

public class PagingParameters
{
    public const int DefaultMaxOnPageCount = 20;
    public const int DefaultPageNumber = 1;
    
    /// <summary>
    /// Items per page
    /// </summary>
    public int Count { get; set; } = DefaultMaxOnPageCount;
    
    /// <summary>
    /// Current page (min value = 1)
    /// </summary>
    public int Page { get; set; } = DefaultPageNumber;
}

/// <summary>
/// Pagination with sorting 
/// </summary>
public class PagingSortParameters : PagingParameters
{
    /// <summary>
    /// Sort by column
    /// </summary>
    public string SortColumn { get; set; }

    /// <summary>
    /// Sort order 0 - ascending, 1 - descending 
    /// </summary>
    public PagingSortOrder SortOrder { get; set; } = PagingSortOrder.Desc;
}

/// <summary>
/// Sort order 0 - ascending, 1 - descending 
/// </summary>
public enum PagingSortOrder
{
    /// <summary>
    /// Ascending 
    /// </summary>
    Asc,

    /// <summary>
    /// Descending
    /// </summary>
    Desc
}