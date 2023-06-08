using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Wallet.Application.Contracts.Persistence;
using Wallet.Application.Extensions;
using Wallet.Application.Responses;

namespace Wallet.Application.Commands.WalletCommands
{
    public class CreateWalletCommand: IRequest<BaseReponse>
    {
        public string Name { get; set; }
        public string UserId { get; set; }
        public string WalletTypeId { get; set; }
        public string AccountSchemeId { get; set; }
    }

    public class CreateWalletCommandHandler : IRequestHandler<CreateWalletCommand, BaseReponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateWalletCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseReponse> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.WalletRepository.HandleCreateAsync(_mapper, request);
        }
    }
}
