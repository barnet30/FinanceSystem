using FinanceSystem.Data.Entities;

namespace FinanceSystem.Data.Interfaces;

public interface IReferenceRepository<T> where T : BaseReferenceEntity
{
    Task<T> GetByIdAsync(int id);
    Task<List<T>> GetAll();
}