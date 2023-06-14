using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wallet.Application.Contracts.Persistence;
using Wallet.Domain.Entities;

namespace Wallet.Infrastructure.Persistence.Repositories
{
    public class WalletRepository : BaseRepositry<HubtelWallet>, IWalletRepository
    {
        public WalletRepository(IMongoDatabase database) : base(database)
        {
            collection = "wallet";
        }

        public Task<IList<HubtelWallet>> GetUserWallets(string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
