using System;
using System.Linq;
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
        public string Id { get; set; }
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

            var result = (await _unitOfWork.WalletTypeRepository
                        .GetAllAsync(e => e.Name.ToLower() == request.Name.ToLower().Trim()))
                        .FirstOrDefault();

            if (result != null && result.Id != request.Id)
                return response.Failed("Update", DuplicateMsg);

            // Delegate task to the general update execution
            return await _unitOfWork.WalletTypeRepository.HandleUpdateAsync(_mapper, request, e => e.Id == request.Id);
        }
    }
}
