using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Wallet.Application.Contracts.Persistence;
using Wallet.Application.DTOs;
using Wallet.Application.Extensions;
using Wallet.Application.Responses;

namespace Wallet.Application.Commands.WalletTypeCommands
{
    public class UpdateAccountTypeCommand : IRequest<BaseReponse>
    {
        public UpdateAccountTypeDTO DTO { get; set; }
    }

    public class UpdateWalletTypeCommandHandler : IRequestHandler<UpdateAccountTypeCommand, BaseReponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private const string TypeExist = "Accout type exists for another record";

        public UpdateWalletTypeCommandHandler(IMapper maaper, IUnitOfWork unitOfWork)
        {
            _mapper = maaper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseReponse> Handle(UpdateAccountTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseReponse();

            var result = (await _unitOfWork.AccountTypeRepository
                        .GetAllAsync(e => e.Name.ToLower() == request.DTO.Name.ToLower().Trim()))
                        .FirstOrDefault();

            if (result != null && result.Id != request.DTO.Id)
                return response.Failed("Update", TypeExist);

            // Delegate task to the general update execution
            return await _unitOfWork.AccountTypeRepository.HandleUpdateAsync(_mapper, request.DTO, e => e.Id == request.DTO.Id);
        }
    }
}
