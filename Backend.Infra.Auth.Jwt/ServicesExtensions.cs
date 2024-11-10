using System.Text;
using Backend.Core.Domain.Auth;
using Backend.Core.Domain.Auth.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Infra.Auth.Jwt;

public static class ServiceExtensions
{
    public static void ConfigureAuthApp(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSection = configuration.GetSection(nameof(JwtSettings));

        var issuer = jwtSection.GetValue<string>("Issuer")
            ?? throw new ArgumentException("Missing Issuer");
        var audience = jwtSection.GetValue<string>("Audience")
            ?? throw new ArgumentException("Missing Audience");
        var secretKey = jwtSection.GetValue<string>("SecretKey")
            ?? throw new ArgumentException("Missing SecretKey");

        services.Configure<JwtSettings>(jwtSection);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
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
                ClockSkew = TimeSpan.Zero,
            };
        });

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
    }
}
