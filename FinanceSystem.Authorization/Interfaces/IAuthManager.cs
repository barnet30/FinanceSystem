using FinanceSystem.Abstractions.Models.Result;
using FinanceSystem.Data.Entities;

namespace Authorization.Interfaces;

public interface IAuthManager
{
    /// <summary>
    /// Generating jwt token for authorized user
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Result<string> GenerateJwt(User user);
}