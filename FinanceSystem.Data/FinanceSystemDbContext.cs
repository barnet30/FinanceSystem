using FinanceSystem.Data.Configurations;
using FinanceSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceSystem.Data;

public class FinanceSystemDbContext : DbContext, IFinanceSystemDbContext
{
    public FinanceSystemDbContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<Location> Locations { get; set; }
    public DbSet<Bank> Banks { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<PaymentCategory> PaymentCategories { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken token = default) => base.SaveChangesAsync(token);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PaymentEntityConfiguration).Assembly);
    }
}