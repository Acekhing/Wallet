using System.Threading;
using System.Threading.Tasks;
using Amazon.Runtime.Internal;
using AutoMapper;
using MediatR;
using Wallet.Application.Contracts.Persistence;
using Wallet.Application.DTOs;
using Wallet.Application.Extensions;
using Wallet.Application.Responses;

namespace Wallet.Application.Commands.WalletTypeCommands
{
    public class CreateAccountTypeCommand : IRequest<BaseReponse>
    {
        public CreateAccountTypeDTO DTO { get; set; }
    }

    public class CreateWalletTypeCommandHandler : IRequestHandler<CreateAccountTypeCommand, BaseReponse>
    {
        private const string TypeExist = "Account type already exist";
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateWalletTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseReponse> Handle(CreateAccountTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseReponse();

            if (await IsTypeExist(request.DTO.Name))
            {
                return response.Failed("Creation", TypeExist);
            }
                
            return await _unitOfWork.AccountTypeRepository.HandleCreateAsync(_mapper, request.DTO);
        }

        private async Task<bool> IsTypeExist(string typename)
        {
            var results = await _unitOfWork.AccountTypeRepository
                                .GetAllAsync(e => e.Name.ToLower() == typename.ToLower().Trim());

            return results.Count > 0;
        }
    }
}
