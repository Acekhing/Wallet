using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Wallet.Application.Contracts.Persistence;
using Wallet.Application.DTOs;
using Wallet.Application.Extensions;
using Wallet.Application.Responses;
using Wallet.Application.Utilities;
using Wallet.Domain.Entities;

namespace Wallet.Application.Commands.WalletCommands
{
    public class UpdateWalletCommand : IRequest<BaseReponse>
    {
        public UpdateWalletDTO DTO { get; set; }
    }

    public class UpdateWalletCommandHandler : IRequestHandler<UpdateWalletCommand, BaseReponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private const string MaxWalletMsg = "You have exceeded your maximum wallet accounts";

        public UpdateWalletCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseReponse> Handle(UpdateWalletCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseReponse();

            var existingWallet = await IsWalletExist(request.DTO.Id);
            if (existingWallet == null)
            {
                return response.Failed("Update", "The wallet you are trying to update does not exist");
            }

            var type = await GetAccountType(request.DTO.AccountTypeId);
            if (type == null)
            {
                return response.Failed("Update", "Account type does not exist");
            }

            var scheme = await GetAccountScheme(request.DTO.AccountTypeId, request.DTO.AccountSchemeId);
            if (scheme == null)
            {
                return response.Failed("Update", "Account scheme does not exist");
            }

            var encryptedAccountNumber = CryptographyUtils.Encrypt(request.DTO.AccountNumber);
            if (await IsAccountNumberExist(encryptedAccountNumber, request.DTO.Id))
            {
                return response.Failed("Update", "Wallet exist with the same account number");
            }

            if (await HasExceededMaxAccount(request.DTO.UserId))
            {
                return response.Failed("Update", MaxWalletMsg);
            }

            SecureAccount(request.DTO, type.Name, scheme.Name, encryptedAccountNumber, out HubtelWallet wallet);

            wallet = _mapper.Map(wallet, existingWallet);

            await _unitOfWork.WalletRepository.UpdateAsync(wallet, e => e.Id == wallet.Id);

            return response.Success(message: "Wallet updated successfully");
        }

        private void SecureAccount(UpdateWalletDTO dto, string type, string scheme, string encryptedNumber, out HubtelWallet wallet)
        {
            wallet = new HubtelWallet
            {
                Id = dto.Id,
                Name = dto.Name,
                AccountScheme = scheme,
                Type = type,
                AccountNumber = dto.AccountNumber.Substring(0, 6),
                EncryptedAccountNumber = encryptedNumber,
                Owner = dto.Owner,
                UserId = dto.UserId,
                EditedAt = dto.EditedAt
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

        private async Task<bool> IsAccountNumberExist(string accountNumber, string walletId)
        {
            var accounts = await _unitOfWork.WalletRepository
                    .GetAllAsync(e => e.EncryptedAccountNumber == accountNumber.Trim());

            return accounts.Count > 0 && accounts.Any(e => e.Id != walletId);
        }

        private async Task<HubtelWallet?> IsWalletExist(string walletId)
        {
            var results = await _unitOfWork.WalletRepository.GetAllAsync(e => e.Id == walletId);
            return results.FirstOrDefault();
        }

        private async Task<bool> HasExceededMaxAccount(string userId)
        {
            var results = await _unitOfWork.WalletRepository.GetAllAsync(e => e.UserId == userId);
            return results != null && results.Count == 5;
        }
    }
}
