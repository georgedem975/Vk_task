using DataAccess.Configuration;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DatabaseContext;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }

    public DbSet<UserGroup> UserGroups { get; set; }

    public DbSet<UserState> UserStates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserGroupConfiguration());
        modelBuilder.ApplyConfiguration(new UserStateConfiguration());
    }
}