using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Contexts
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = Guid.NewGuid(),
                    Name = "Guest",
                    NormalizedName = "GUEST",
                    Email = "guest@guest.com",
                    Password = "$2a$11$K4CjmGjTWwjpQTjyw/bmouNMUtwtpzgjPOVFIPAazaVHI9YgAc1Lq", // guest
                    Role = Role.GUEST,
                    CreatedAt = DateTime.Now,
                    LastLogin = DateTime.Now,
                    VerifiedAt = DateTime.Now,
                }
            );
        }
    }
}