using FinanceSystem.Data.Entities;
using FinanceSystem.Data.Interfaces.Users;
using Microsoft.EntityFrameworkCore;

namespace FinanceSystem.Data.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(IFinanceSystemDbContext context) : base(context)
    {
    }

    public async Task<User> GetByEmailAndPassword(string email, string password)
    {
        return await DbSet.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
    }
}