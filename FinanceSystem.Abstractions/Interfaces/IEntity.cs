namespace FinanceSystem.Abstractions.Interfaces;

public interface IEntity
{
    /// <summary>
    /// Entity Id
    /// </summary>
    public Guid Id { get; set; }
}