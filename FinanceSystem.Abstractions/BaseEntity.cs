using FinanceSystem.Abstractions.Interfaces;

namespace FinanceSystem.Abstractions;

/// <summary>
/// Base class for entities
/// </summary>
public abstract class BaseEntity : IEntity, ISoftDelete
{
    /// <summary>
    /// Entity Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Shows if entity was deleted
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Date time when entity was deleted
    /// </summary>
    public DateTime? DeletedAt { get; set; }
}