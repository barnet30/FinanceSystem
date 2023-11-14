using System.Linq.Expressions;
using Ardalis.Specification;
using FinanceSystem.Data.Entities;

namespace FinanceSystem.Data.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<IQueryable<T>> QueryAsync(Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
    Task<IQueryable<T>> QueryAsync(ISpecification<T> specification);
    Task<T> GetSingleAsync(ISpecification<T> specification);
    Task<T> GetByIdAsync(Guid id);
    Task<Guid> InsertAsync(T entity);
    Task UpdateAsync(T entityToUpdate);
    Task DeleteAsync(T entity);
    Task DeleteAsync(Guid id);
}