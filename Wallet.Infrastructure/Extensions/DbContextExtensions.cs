using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using Wallet.Infrastructure.Persistence.Data;

namespace Wallet.Infrastructure.Extensions
{
    public static class DbContextExtensions
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            //Seeding roles
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Id = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                },
                new IdentityRole()
                {
                    Id = "2c5e174e-3b0e-446f-86af-483d56fd7ddd",
                    Name = "User",
                    NormalizedName = "User".ToUpper()
                }
                );
           
            var hasher = new PasswordHasher<IdentityUser>();

            // Seeding Admin User 
            modelBuilder.Entity<HubtelUser>().HasData(
               new HubtelUser()
               {
                   Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                   UserName = "admin@hubtel.com",
                   Email = "admin@hubtel.com",
                   NormalizedUserName = "Admin@hubtel.com".ToUpper(),
                   NormalizedEmail = "Admin@hubtel.com".ToUpper(),
                   PasswordHash = hasher.HashPassword(null, "admin"),
                   EmailConfirmed = true,
                   LockoutEnabled = true,
                   PhoneNumberConfirmed = true,
                   SecurityStamp = Guid.NewGuid().ToString()
               }
                );

            //Seeding the relation between admin user and role

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>()
                {
                    RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9" 
                }
                );
        }
    }
}
