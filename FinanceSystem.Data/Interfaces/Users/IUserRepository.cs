using FinanceSystem.Data.Entities;

namespace FinanceSystem.Data.Interfaces.Users;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetByEmailAndPassword(string email, string password);
}