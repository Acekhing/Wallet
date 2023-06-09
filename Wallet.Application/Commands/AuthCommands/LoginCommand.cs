using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Wallet.Application.Contracts.Persistence;
using Wallet.Application.DTOs.AuthModels;
using Wallet.Application.Responses;

namespace Wallet.Application.Commands.AuthCommands
{
    public class LoginCommand: IRequest<LoginResponse>
    {
        public LoginDto LoginDto { get; set; }
    }

    public class LoginCommandHandler: IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoginCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.AuthService.LoginAsync(request.LoginDto);
        }
    }
}
