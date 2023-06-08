using Wallet.Domain.Entities.WalletEntities;

namespace Wallet.Application.Contracts.Persistence
{
    public interface IWalletRepository: IBaseRepository<HubtelWallet>
    {
    }
}
