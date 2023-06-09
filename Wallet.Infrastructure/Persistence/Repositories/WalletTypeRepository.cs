using MongoDB.Driver;
using Wallet.Application.Contracts.Persistence;
using Wallet.Domain.Entities.WalletEntities;

namespace Wallet.Infrastructure.Persistence.Repositories
{
    public class WalletTypeRepository : BaseRepositry<WalletType>, IWalletTypeRepository
    {
        public WalletTypeRepository(IMongoDatabase database) : base(database)
        {
            collection = "wallet_type";
        }
    }
}
