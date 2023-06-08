using System.Collections.Generic;
using System.Threading.Tasks;
using Wallet.Domain.Entities.WalletEntities;

namespace Wallet.Application.Contracts.Persistence
{
    public interface IWalletRepository : IBaseRepository<HubtelWallet>
    {
        public Task<IList<HubtelWallet>> GetUserWallets(string userId);
    }
}
