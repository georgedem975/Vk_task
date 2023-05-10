using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Login)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.Password)
            .IsRequired();

        builder.Property(u => u.CreatedDate)
            .IsRequired();

        builder.HasOne(u => u.UserGroup)
            .WithOne(g => g.User)
            .HasForeignKey<UserGroup>(u => u.Id)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(u => u.UserState)
            .WithOne(s => s.User)
            .HasForeignKey<UserState>(u => u.Id)
            .OnDelete(DeleteBehavior.Restrict);
    }
}