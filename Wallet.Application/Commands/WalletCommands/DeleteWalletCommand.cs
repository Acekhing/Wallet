using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Wallet.Application.Contracts.Persistence;
using Wallet.Application.Extensions;
using Wallet.Application.Responses;

namespace Wallet.Application.Commands.WalletCommands
{
    public class DeleteWalletCommand : IRequest<BaseReponse>
    {
        public string Id { get; set; }
    }

    public class DeleteWalletCommandHandler : IRequestHandler<DeleteWalletCommand, BaseReponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteWalletCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseReponse> Handle(DeleteWalletCommand request, CancellationToken cancellationToken)
        {
            // Delegate task to the general create execution
            return await _unitOfWork.WalletRepository.HandleDeleteAsync(request.Id);
        }
    }
}
