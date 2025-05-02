using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ExpenseTracker.Api.Domain;
using ExpenseTracker.Base;

namespace ExpenseTracker.Api.Service;

public class TokenService:ITokenService{

    private readonly JwtConfig jwtConfig;

    public TokenService(JwtConfig jwtConfig)
    {
        this.jwtConfig = jwtConfig;
    }
    public string GenerateToken(Personnel personnel)
    {
        var claims = GetClaims(personnel);
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtConfig.Issuer,
            audience: jwtConfig.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    private Claim[] GetClaims(Personnel personnel)
    {
        var claims = new List<Claim>
        {
            new Claim("Role", personnel.Role),
            new Claim("FirstName", personnel.FirstName),
            new Claim("LastName", personnel.LastName),
            new Claim("UserName", personnel.UserName),
            new Claim("personnelId", personnel.Id.ToString()),
            new Claim("Secret", personnel.PersonnelPassword.Secret.ToString())
        };
        return claims.ToArray();
    }
}