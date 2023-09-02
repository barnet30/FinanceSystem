using FinanceSystem.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceSystem.Data.Configurations;

public class BankEntityConfiguration : IEntityTypeConfiguration<BankEntity>
{
    public void Configure(EntityTypeBuilder<BankEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.HasMany(x => x.Payments)
            .WithOne(x => x.Bank)
            .OnDelete(DeleteBehavior.Cascade);
    }
}