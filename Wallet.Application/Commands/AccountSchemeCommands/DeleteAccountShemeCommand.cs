using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Wallet.Application.Contracts.Persistence;
using Wallet.Application.Extensions;
using Wallet.Application.Responses;

namespace Wallet.Application.Commands.AccountSchemeCommands
{
    public class DeleteAccountShemeCommand : IRequest<BaseReponse>
    {
        public string Id { get; set; }
    }

    public class DeleteAccountShemeCommandHandler : IRequestHandler<DeleteAccountShemeCommand, BaseReponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAccountShemeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseReponse> Handle(DeleteAccountShemeCommand request, CancellationToken cancellationToken)
        {
            // Delegate task to the general create execution
            return await _unitOfWork.AccountSchemeRepository.HandleDeleteAsync(request.Id);
        }
    }
}
