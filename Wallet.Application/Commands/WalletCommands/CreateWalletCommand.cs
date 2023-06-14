using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wallet.Application.Contracts.Persistence;
using Wallet.Application.DTOs;
using Wallet.Application.Extensions;
using Wallet.Application.Responses;
using Wallet.Application.Utilities;
using Wallet.Domain.Entities;

namespace Wallet.Application.Commands.WalletCommands
{
    public class CreateWalletCommand : IRequest<BaseReponse>
    {
        public CreateWalletDTO DTO { get; set; }
    }

    public class CreateWalletCommandHandler : IRequestHandler<CreateWalletCommand, BaseReponse>
    {
        private const string MaxWalletMsg = "You have exceeded your maximum wallet accounts";
        private readonly IUnitOfWork _unitOfWork;

        public CreateWalletCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<BaseReponse> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseReponse();

            var type = await GetAccountType(request.DTO.AccountTypeId);
            if (type == null)
            {
                return response.Failed("Creation", "Account type does not exist");
            }

            var scheme = await GetAccountScheme(request.DTO.AccountTypeId,request.DTO.AccountSchemeId);
            if(scheme == null)
            {
                return response.Failed("Creation", "Account scheme does not exist");
            }

            var encryptedAccountNumber = CryptographyUtils.Encrypt(request.DTO.AccountNumber);
            if (await IsAccountNumberExist(encryptedAccountNumber))
            {
                return response.Failed("Creation", "Wallet exist with the same account number");
            }

            if (await HasExceededMaxAccount(request.DTO.UserId))
            {
                return response.Failed("Creation", MaxWalletMsg);
            }

            SecureAccount(request.DTO,type.Name, scheme.Name,encryptedAccountNumber, out HubtelWallet wallet);
            
            await _unitOfWork.WalletRepository.CreateAsync(wallet);

            return response.Success(message: "Wallet created successfully");
        }

        private void SecureAccount(CreateWalletDTO dto, string type, string scheme,string encryptedNumber, out HubtelWallet wallet)
        {
            wallet = new HubtelWallet
            {
               Name = dto.Name,
               AccountScheme = scheme,
               Type = type,
               AccountNumber= dto.AccountNumber.Substring(0, 6),
               EncryptedAccountNumber = encryptedNumber,
               Owner = dto.Owner,
               UserId = dto.UserId
            };
        }

        private async Task<AccountType?> GetAccountType(string typeId)
        {
            return (await _unitOfWork.AccountTypeRepository.GetAllAsync(e => e.Id == typeId)).FirstOrDefault();
        }

        private async Task<AccountScheme?> GetAccountScheme(string typeId, string schemeId)
        {
            var results = await _unitOfWork.AccountSchemeRepository.GetAllAsync(e => e.AccountTypeId == typeId && e.Id == schemeId);
            return results.FirstOrDefault();
        }

        private async Task<bool> IsAccountNumberExist(string accountNumber)
        {
            var accounts = await _unitOfWork.WalletRepository
                    .GetAllAsync(e => e.EncryptedAccountNumber == accountNumber.Trim());

            return accounts.Count > 0;
        }

        private async Task<bool> HasExceededMaxAccount(string userId)
        {
            var results = await _unitOfWork.WalletRepository.GetAllAsync(e => e.UserId == userId);
            return results != null && results.Count == 5;
        }
    }
}
