using System.Diagnostics.CodeAnalysis;
using FinanceSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FinanceSystem.Data;

public interface IDbContext
{
    DbSet<Location> Locations { get; set; }
    DbSet<Bank> Banks { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<Company> Companies { get; set; }
    DbSet<Payment> Payments { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken token = default);
    EntityEntry<TEntity> Entry<TEntity>([NotNull] TEntity entity)
        where TEntity : class;
}