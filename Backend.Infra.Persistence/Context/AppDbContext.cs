using Backend.Core.Domain.Entities;
using Backend.Infra.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infra.Persistence.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
    }

    public required DbSet<User> Users { get; set; }
}
