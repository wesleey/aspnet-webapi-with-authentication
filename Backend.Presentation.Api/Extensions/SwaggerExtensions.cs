using Microsoft.OpenApi.Models;

namespace Backend.Presentation.Api.Extensions;

public static class SwaggerExtensions
{
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Scheme = "Bearer",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Name = "Bearer",
                        Scheme = "oauth2",
                        In = ParameterLocation.Header,
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme,
                        }
                    },
                    new List<string>()
                }
            });
        });
    }
}
