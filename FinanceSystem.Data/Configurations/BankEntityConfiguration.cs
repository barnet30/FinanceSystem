using FinanceSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceSystem.Data.Configurations;

public class BankEntityConfiguration : IEntityTypeConfiguration<Bank>
{
    public void Configure(EntityTypeBuilder<Bank> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.HasMany(x => x.Payments)
            .WithOne(x => x.Bank)
            .OnDelete(DeleteBehavior.Cascade);
    }
}