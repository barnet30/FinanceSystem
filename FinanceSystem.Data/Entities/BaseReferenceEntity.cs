namespace FinanceSystem.Data.Entities;

/// <summary>
/// Base class for reference entities
/// </summary>
public abstract class BaseReferenceEntity
{
    /// <summary>
    /// Entity Id
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }
}