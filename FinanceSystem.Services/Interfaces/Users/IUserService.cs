using FinanceSystem.Abstractions.Models.Result;
using FinanceSystem.Abstractions.Models.Users;

namespace FinanceSystem.Services.Interfaces.Users;

public interface IUserService
{
    Task<Result<Guid>> RegisterUser(UserRegisterDto userRegisterDto);
}