using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend.Core.Domain.Enums;
using Backend.Infra.Auth.Jwt.Configurations;
using Backend.Infra.Auth.Jwt.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Infra.Auth.Jwt;

public class JwtTokenGenerator(IOptions<JwtSettings> jwtSettings) : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;

    public string Generate(int id, string email, Role role)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new Claim[]
        {
            new(JwtRegisteredClaimNames.Sub, id.ToString()),
            new(JwtRegisteredClaimNames.Email, email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Typ, "at+jwt"),
            new(ClaimTypes.Role, role.ToString())
        };

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: credentials,
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: DateTime.Now.AddMinutes(60),
            notBefore: DateTime.Now
        );

        var handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(token);
    }

    public bool Validate(string token)
    {
        var parameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            RequireSignedTokens = true,
            ValidIssuer = _jwtSettings.Issuer,
            ValidAudience = _jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)),
            RoleClaimType = ClaimTypes.Role,
            ClockSkew = TimeSpan.Zero,
        };

        var handler = new JwtSecurityTokenHandler();

        try
        {
            handler.ValidateToken(token, parameters, out _);
            return true;
        }
        catch (SecurityTokenException)
        {
            return false;
        }
    }

    public bool IsAdmin(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var claims = handler.ReadJwtToken(token).Claims;
        var role = claims.First(x => x.Type == ClaimTypes.Role).Value;
        return role == Role.Admin.ToString();
    }

    public int GetId(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var claims = handler.ReadJwtToken(token).Claims;
        var sub = claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;
        return int.Parse(sub);
    }
}
