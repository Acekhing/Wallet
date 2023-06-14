using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Wallet.Application.Contracts.Persistence;
using Wallet.Application.Extensions;
using Wallet.Application.Responses;

namespace Wallet.Application.Commands.WalletTypeCommands
{
    public class DeleteAccountTypeCommand : IRequest<BaseReponse>
    {
        public string Id { get; set; }
    }

    public class DeleteWalletTypeCommandHandler : IRequestHandler<DeleteAccountTypeCommand, BaseReponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteWalletTypeCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<BaseReponse> Handle(DeleteAccountTypeCommand request, CancellationToken cancellationToken)
        {
            // Delegate task to the geneal delete function handler
            return await _unitOfWork.AccountTypeRepository.HandleDeleteAsync(e => e.Id == request.Id);
        }
    }
}
