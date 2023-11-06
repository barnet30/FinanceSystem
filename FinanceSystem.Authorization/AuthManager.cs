using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Authorization.Configuration;
using Authorization.Interfaces;
using FinanceSystem.Abstractions.Models.Result;
using FinanceSystem.Common.Constants;
using FinanceSystem.Data.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Authorization;

public sealed class AuthManager : IAuthManager
{
    private readonly AuthOptions _authOptions;

    public AuthManager(IOptions<AuthOptions> authOptions)
    {
        _authOptions = authOptions.Value;
    }

    public Result<string> GenerateJwt(User user)
    {
        var claims = new List<Claim>()
        {
            new(AuthorizationConstants.EmailClaimName, user.Email),
            new(AuthorizationConstants.PhoneClaimName, user.Phone),
            new(AuthorizationConstants.UserIdClaimName, user.Phone),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.Key));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_authOptions.Lifetime),
            signingCredentials: cred);

        return Result<string>.FromValue(new JwtSecurityTokenHandler().WriteToken(token));
    }
}