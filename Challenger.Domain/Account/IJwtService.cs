using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Challenger.Domain.Account
{
    public interface IJwtService
    {
        JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims);
        List<Claim> GetClaims(string userEmail);
        SigningCredentials GetSigningCredentials();
    }
}