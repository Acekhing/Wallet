using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Wallet.Application.Contracts.Persistence;
using Wallet.Application.Extensions;
using Wallet.Application.Responses;

namespace Wallet.Application.Commands.WalletCommands
{
    public class CreateWalletCommand : IRequest<BaseReponse>
    {
        public string Name { get; set; }
        public string UserId { get; set; }
        public string WalletTypeId { get; set; }
        public string AccountSchemeId { get; set; }
        public string AccountNumber { get; set; }
    }

    public class CreateWalletCommandHandler : IRequestHandler<CreateWalletCommand, BaseReponse>
    {
        private const string MaxWalletMsg = "You have exceeded your maximum wallet accounts";
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateWalletCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseReponse> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseReponse();

            if (await IsValidAccountScheme(request.AccountSchemeId) == false)
                return response.Failed("Creation","Account scheme does not exist");

            if (await IsValidWalletType(request.WalletTypeId) == false)
                return response.Failed("Creation", "Wallet type does not exist");

            var results = await _unitOfWork.WalletRepository.GetAllAsync(e => e.UserId == request.UserId);

            if (results != null && results.Count == 5)
                return response.Failed("Creation", MaxWalletMsg);

            // Delegate task to the general create execution
            return await _unitOfWork.WalletRepository.HandleCreateAsync(_mapper, request);
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
