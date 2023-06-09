using System;
using Wallet.Application.Contracts.Auth;

namespace Wallet.Application.Contracts.Persistence
{
    public interface IUnitOfWork: IDisposable
    {
        public IWalletRepository WalletRepository { get; }
        public IWalletTypeRepository WalletTypeRepository { get; }
        public IAccountSchemeRepository AccountSchemeRepository { get; }
        public IAuthService AuthService { get; }
    }
}
