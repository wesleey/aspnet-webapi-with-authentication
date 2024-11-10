using Backend.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infra.Persistence.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}
