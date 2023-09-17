using FinanceSystem.Data.Entities;
using FinanceSystem.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinanceSystem.Data.Repositories;

public class UserRepository : BaseRepository<User>, IRepository<User>
{
    public UserRepository(DbSet<User> dbSet, IDbContext context) : base(dbSet, context)
    {
    }
}