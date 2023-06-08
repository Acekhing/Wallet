using System;

namespace Wallet.Application.Contracts.Persistence
{
    public interface IUnitOfWork: IDisposable
    {
        public IWalletRepository WalletRepository { get; }
        public IWalletTypeRepository WalletTypeRepository { get; }
        public IAccountSchemeRepository AccountSchemeRepository { get; }
    }
}
