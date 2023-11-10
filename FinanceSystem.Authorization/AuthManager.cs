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
            new(AuthorizationConstants.UserIdClaimName, user.Id.ToString()),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.Key));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_authOptions.Lifetime),
            Issuer = _authOptions.Issuer,
            Audience = _authOptions.Audience,
            SigningCredentials = cred
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return Result<string>.FromValue(tokenHandler.WriteToken(token));
    }
}