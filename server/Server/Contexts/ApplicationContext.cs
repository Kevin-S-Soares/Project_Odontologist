using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Contexts;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<HashStorage> HashStorage { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string? connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
        if (connectionString is not null)
        {
            optionsBuilder.UseSqlite(connectionString);
        }
    }
}
