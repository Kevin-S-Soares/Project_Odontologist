using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Contexts;

public class ApplicationContext : DbContext
{
    public ApplicationContext() { }
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<HashStorage> HashStorage { get; set; }
    public virtual DbSet<Odontologist> Odontologists { get; set; }
    public virtual DbSet<Appointment> Appointments { get; set; }
    public virtual DbSet<BreakTime> BreakTimes { get; set; }
    public virtual DbSet<Schedule> Schedules { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string? connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
        if (connectionString is not null)
        {
            optionsBuilder.UseSqlite(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Seed();
    }
}
