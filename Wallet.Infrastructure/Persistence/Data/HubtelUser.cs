using Microsoft.AspNetCore.Identity;

namespace Wallet.Infrastructure.Persistence.Data
{
    public class HubtelUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
