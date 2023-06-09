using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Wallet.Application.Contracts.Persistence;
using Wallet.Application.Extensions;
using Wallet.Application.Responses;

namespace Wallet.Application.Commands.WalletCommands
{
    public class UpdateWalletCommand : IRequest<BaseReponse>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public string WalletTypeId { get; set; }
        public string AccountSchemeId { get; set; }
        public string AccountNumber { get; set; }
        public DateTime EditedAt { get; set; }
    }

    public class UpdateWalletCommandHandler : IRequestHandler<UpdateWalletCommand, BaseReponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateWalletCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseReponse> Handle(UpdateWalletCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseReponse();

            if (await IsValidAccountScheme(request.AccountSchemeId) == false)
                return response.Failed("Update", "Account scheme does not exist");

            if (await IsValidWalletType(request.WalletTypeId) == false)
                return response.Failed("Update", "Wallet type does not exist");

            // Delegate task to the general update execution
            return await _unitOfWork.WalletRepository.HandleUpdateAsync(_mapper, request, e => e.Id == request.Id);
        }

        private async Task<bool> IsValidAccountScheme(string schemeId)
        {
            var schemes = await _unitOfWork.AccountSchemeRepository.GetAllAsync(e => e.Id == schemeId);

            return schemes.Count > 0;

        }

        private async Task<bool> IsValidWalletType(string typeIs)
        {
            var schemes = await _unitOfWork.WalletTypeRepository.GetAllAsync(e => e.Id == typeIs);

            return schemes.Count > 0;
        }
    }
}
