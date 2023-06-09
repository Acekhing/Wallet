using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Wallet.Application.Contracts.Persistence;
using Wallet.Application.DTOs.AuthModels;
using Wallet.Application.Responses;

namespace Wallet.Application.Commands.AuthCommands
{
    public class RegisterCommand: IRequest<BaseReponse>
    {
        public RegisterDto RegisterDto { get; set; }
    }

    public class RegisterCommandHandler: IRequestHandler<RegisterCommand, BaseReponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseReponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.AuthService.RegisterAsync(request.RegisterDto);
        }
    }
}
