using System;
using Wallet.Application.Contracts.Auth;
using Wallet.Application.Contracts.Persistence;

namespace Wallet.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IWalletTypeRepository _walletTypeRepository;
        private readonly IAccountSchemeRepository _accountSchemeRepository;
        private readonly IAuthService _authService;

        public UnitOfWork(
            IWalletRepository walletRepository, 
            IWalletTypeRepository walletTypeRepository, 
            IAccountSchemeRepository accountSchemeRepository,
            IAuthService authService)
        {
            _walletRepository = walletRepository;
            _walletTypeRepository = walletTypeRepository;
            _accountSchemeRepository = accountSchemeRepository;
            _authService = authService; 
        }

        public IWalletRepository WalletRepository => _walletRepository;

        public IWalletTypeRepository WalletTypeRepository => _walletTypeRepository;

        public IAccountSchemeRepository AccountSchemeRepository => _accountSchemeRepository;

        public IAuthService AuthService => _authService;

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
