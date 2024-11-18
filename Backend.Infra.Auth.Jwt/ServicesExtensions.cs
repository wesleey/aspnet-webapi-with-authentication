using System.Security.Claims;
using System.Text;
using Backend.Core.Domain.Enums;
using Backend.Infra.Auth.Jwt.Configurations;
using Backend.Infra.Auth.Jwt.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Infra.Auth.Jwt;

public static class ServiceExtensions
{
    public static void ConfigureAuthApp(this IServiceCollection services)
    {
        var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER")
            ?? throw new ArgumentException("Missing JWT_ISSUER");

        var audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE")
            ?? throw new ArgumentException("Missing JWT_AUDIENCE");

        var secretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY")
            ?? throw new ArgumentException("Missing JWT_SECRET_KEY");

        services.Configure<JwtSettings>(options =>
        {
            options.Issuer = issuer;
            options.Audience = audience;
            options.SecretKey = secretKey;
        });

        services.AddAuthentication(options =>
        {
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                RequireSignedTokens = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                RoleClaimType = ClaimTypes.Role,
                ClockSkew = TimeSpan.Zero,
            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy =>
                policy.RequireRole(Role.Admin.ToString()));

            options.AddPolicy("User", policy =>
                policy.RequireRole(
                    Role.User.ToString(),
                    Role.Admin.ToString()
                ));
        });

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
    }
}
