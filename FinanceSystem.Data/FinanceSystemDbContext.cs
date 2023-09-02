using FinanceSystem.Abstractions.Entities;
using FinanceSystem.Data.Configurations;
using FinanceSystem.Data.Helpers;
using Microsoft.EntityFrameworkCore;

namespace FinanceSystem.Data;

public class FinanceSystemDbContext : DbContext, IDbContext
{
    public FinanceSystemDbContext(DbContextOptions<FinanceSystemDbContext> options) : base(options)
    {
        
    }

    public DbSet<LocationEntity> Locations { get; set; }
    public DbSet<BankEntity> Banks { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<CompanyEntity> Companies { get; set; }
    public DbSet<PaymentEntity> Payments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.AddInterceptors(new SoftDeleteInterceptor());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PaymentEntityConfiguration).Assembly);
    }
}