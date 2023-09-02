using FinanceSystem.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceSystem.Data;

public interface IDbContext
{
    DbSet<LocationEntity> Locations { get; set; }
    DbSet<BankEntity> Banks { get; set; }
    DbSet<UserEntity> Users { get; set; }
    DbSet<CompanyEntity> Companies { get; set; }
    DbSet<PaymentEntity> Payments { get; set; }
}