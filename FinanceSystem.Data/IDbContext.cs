using FinanceSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceSystem.Data;

public interface IDbContext
{
    DbSet<Location> Locations { get; set; }
    DbSet<Bank> Banks { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<Company> Companies { get; set; }
    DbSet<Payment> Payments { get; set; }
}