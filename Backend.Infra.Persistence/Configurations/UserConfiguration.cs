using Backend.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Infra.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.HasIndex(x => x.Email).IsUnique();
        builder.Property(x => x.Email)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Password)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Role)
            .HasConversion<string>();

        builder.Property(x => x.Created)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(x => x.Updated)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}
