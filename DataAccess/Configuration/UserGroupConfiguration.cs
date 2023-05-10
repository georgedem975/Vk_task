using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configuration;

public class UserGroupConfiguration : IEntityTypeConfiguration<UserGroup>
{
    public void Configure(EntityTypeBuilder<UserGroup> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Code)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(s => s.Description)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.HasOne(u => u.User)
            .WithOne(s => s.UserGroup)
            .HasForeignKey<User>(u => u.UserGroupId);
    }
}