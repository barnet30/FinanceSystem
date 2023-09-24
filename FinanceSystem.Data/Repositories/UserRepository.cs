using FinanceSystem.Data.Entities;
using FinanceSystem.Data.Interfaces.Users;

namespace FinanceSystem.Data.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(IFinanceSystemDbContext context) : base(context)
    {
    }
}