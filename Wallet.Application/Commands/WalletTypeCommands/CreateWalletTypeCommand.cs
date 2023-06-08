using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Wallet.Application.Contracts.Persistence;
using Wallet.Application.Extensions;
using Wallet.Application.Responses;

namespace Wallet.Application.Commands.WalletTypeCommands
{
    public class CreateWalletTypeCommand : IRequest<BaseReponse>
    {
        public string Name { get; set; }
    }

    public class CreateWalletTypeCommandHandler : IRequestHandler<CreateWalletTypeCommand, BaseReponse>
    {
        private const string DuplicateMsg = "Wallet type name exists for another record";
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateWalletTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseReponse> Handle(CreateWalletTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseReponse();

            // Get existing wallet type
            var type = await _unitOfWork.WalletTypeRepository.GetByNameAsync(request.Name);

            if (type != null) // Checking if wallet type already exist. 
                return response.Failed("Creation", DuplicateMsg);

            // Delegate task to the general create execution
            return await _unitOfWork.WalletTypeRepository.HandleCreateAsync(_mapper, request);
        }
    }
}
