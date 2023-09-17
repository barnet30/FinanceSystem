using FinanceSystem.Abstractions.Models.Users;

namespace FinanceSystem.Services.Interfaces.Users;

public interface IUserService
{
    Task<Guid> RegisterUser(UserRegisterDto userRegisterDto);
}