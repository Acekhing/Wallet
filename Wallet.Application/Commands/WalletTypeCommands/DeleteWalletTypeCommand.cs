using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Wallet.Application.Contracts.Persistence;
using Wallet.Application.Responses;

namespace Wallet.Application.Commands.WalletTypeCommands
{
    public class DeleteWalletTypeCommand : IRequest<BaseReponse>
    {
    }

    public class DeleteWalletTypeCommandHandler : IRequestHandler<DeleteWalletTypeCommand, BaseReponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteWalletTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<BaseReponse> Handle(DeleteWalletTypeCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
