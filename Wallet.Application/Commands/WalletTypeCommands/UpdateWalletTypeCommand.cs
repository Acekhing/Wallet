using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Wallet.Application.Contracts.Persistence;
using Wallet.Application.Extensions;
using Wallet.Application.Responses;

namespace Wallet.Application.Commands.WalletTypeCommands
{
    public class UpdateWalletTypeCommand : IRequest<BaseReponse>
    {
        public string Id { get; private set; }
        public string Name { get; set; }
        public DateTime EditedAt { get; set; }
    }

    public class UpdateWalletTypeCommandHandler : IRequestHandler<UpdateWalletTypeCommand, BaseReponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private const string DuplicateMsg = "Wallet type name exists for another record";

        public UpdateWalletTypeCommandHandler(IMapper maaper, IUnitOfWork unitOfWork)
        {
            _mapper = maaper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseReponse> Handle(UpdateWalletTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseReponse();

            // Get existing wallet type
            var type = await _unitOfWork.WalletTypeRepository.GetByNameAsync(request.Name);

            if (type != null && type.Id != request.Id) // Checking if wallet type already exist. 
                return response.Failed("Update", DuplicateMsg);

            // Delegate task to the general update execution
            return await _unitOfWork.WalletTypeRepository.HandleUpdateAsync(_mapper, request, request.Id);
        }
    }
}
