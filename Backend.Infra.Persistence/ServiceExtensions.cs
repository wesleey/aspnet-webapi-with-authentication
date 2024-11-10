using Backend.Core.Domain.Interfaces;
using Backend.Infra.Persistence.Context;
using Backend.Infra.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Infra.Persistence;

public static class ServiceExtensions
{
    public static void ConfigurePersistenceApp(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Sqlite");
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(connectionString));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}
