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
        public string Id { get; private set; }
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
            // Delegate task to the general update execution
            return await _unitOfWork.WalletRepository.HandleUpdateAsync(_mapper, request, request.Id);
        }
    }
}
