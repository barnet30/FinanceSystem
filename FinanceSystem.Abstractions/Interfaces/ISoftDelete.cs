namespace FinanceSystem.Abstractions.Interfaces;

/// <summary>
/// Soft delete entites
/// </summary>
public interface ISoftDelete
{
    /// <summary>
    /// Shows if entity was deleted
    /// </summary>
    public bool IsDeleted { get; set; }
    
    /// <summary>
    /// Date time when entity was deleted
    /// </summary>
    public DateTime? DeletedAt { get; set; }
}