using FinanceSystem.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FinanceSystem.Data.Helpers;

public class SoftDeleteInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context is null) return result;

        foreach (var entityEntry in eventData.Context.ChangeTracker.Entries())
        {
            if (entityEntry.State != EntityState.Deleted && entityEntry.Entity is not ISoftDelete) continue;
            
            entityEntry.State = EntityState.Modified;
            (entityEntry.Entity as ISoftDelete)!.IsDeleted = true;
            (entityEntry.Entity as ISoftDelete)!.DeletedAt = DateTime.UtcNow;
        }

        return result;
    }
}