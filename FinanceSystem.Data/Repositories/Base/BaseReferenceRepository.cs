using FinanceSystem.Data.Entities;
using FinanceSystem.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinanceSystem.Data.Repositories.Base;

public class BaseReferenceRepository<T> : IReferenceRepository<T> where T : BaseReferenceEntity
{
    private readonly IFinanceSystemDbContext _context;
    protected readonly DbSet<T> DbSet;

    public BaseReferenceRepository(IFinanceSystemDbContext context)
    {
        _context = context;
        DbSet = context.Set<T>();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await DbSet.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<T>> GetAll()
    {
        return await DbSet.ToListAsync();
    }
}