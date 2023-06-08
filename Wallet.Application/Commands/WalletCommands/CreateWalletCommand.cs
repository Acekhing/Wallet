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
            var results = await _unitOfWork.WalletRepository.GetUserWallets(request.UserId);

            if (results != null && results.Count == 5)
                return new BaseReponse { Message = "Action failed.", Success = false, Errors = new List<string> { MaxWalletMsg } };

            return await _unitOfWork.WalletRepository.HandleCreateAsync(_mapper, request);
        }
    }
}
