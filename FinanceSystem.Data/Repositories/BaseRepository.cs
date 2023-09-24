using System.Linq.Expressions;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using FinanceSystem.Data.Entities;
using FinanceSystem.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinanceSystem.Data.Repositories;

public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly IFinanceSystemDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public BaseRepository(IFinanceSystemDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<IQueryable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
    {
        var query = _dbSet.AsQueryable();
        if (filter != null)
            query = query.Where(filter);

        if (orderBy != null)
            query = orderBy(query);

        return await Task.FromResult(query);
    }

    public async Task<IQueryable<TEntity>> QueryAsync(ISpecification<TEntity> specification)
    {
        var query = SpecificationEvaluator.Default.GetQuery(
            query: _dbSet.AsQueryable(),
            specification: specification);

        return await Task.FromResult(query);
    }

    public async Task<TEntity> GetSingleAsync(ISpecification<TEntity> specification)
    {
        var query = SpecificationEvaluator.Default.GetQuery(
            query: _dbSet.AsQueryable(),
            specification: specification);

        return await query.FirstOrDefaultAsync();
    }

    public async Task<TEntity> GetByIdAsync(Guid id)
    {
        var query = await QueryAsync(x => x.Id == id);
        return await query.FirstOrDefaultAsync();
    }

    public async Task<Guid> InsertAsync(TEntity entity)
    {
        entity.Id = Guid.NewGuid();
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task UpdateAsync(TEntity entityToUpdate)
    {
        var entity = await _dbSet.FindAsync(entityToUpdate.Id);
        if (entity == null)
            throw new ArgumentException($"Entity with id {entityToUpdate.Id} was not found");
        _context.Entry(entity).CurrentValues.SetValues(entityToUpdate);
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        if (entity == null)
            return;
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entityToDelete = await _dbSet.FindAsync(id);
        if (entityToDelete != null)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
                _dbSet.Attach(entityToDelete);

            _dbSet.Remove(entityToDelete);
            await _context.SaveChangesAsync();
        }
    }
}