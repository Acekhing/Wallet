using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Wallet.Infrastructure.Extensions;

namespace Wallet.Infrastructure.Persistence.Data
{
    public class HubtelDbContext : IdentityDbContext<HubtelUser>
    {
        public HubtelDbContext(DbContextOptions<HubtelDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed admin info
            builder.SeedData();
        }
    }
}
