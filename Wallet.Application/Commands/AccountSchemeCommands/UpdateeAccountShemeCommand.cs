using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Runtime.Internal;
using AutoMapper;
using MediatR;
using Wallet.Application.Contracts.Persistence;
using Wallet.Application.DTOs;
using Wallet.Application.Extensions;
using Wallet.Application.Responses;

namespace Wallet.Application.Commands.AccountSchemeCommands
{
    public class UpdateeAccountShemeCommand : IRequest<BaseReponse>
    {
        public UpdateAccountSchemeDTO DTO { get; set; }
    }

    public class UpdateeAccountShemeCommandHandler : IRequestHandler<UpdateeAccountShemeCommand, BaseReponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private const string DuplicateMsg = "Account scheme type name exists for another record";

        public UpdateeAccountShemeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseReponse> Handle(UpdateeAccountShemeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseReponse();

            if (await IsWalletTypeExist(request.DTO.AccountTypeId) == false)
                return response.Failed("Update", "Account type does not exist");

            if (await IsSchemeExist(request.DTO.Name, request.DTO.Id) == true)
                return response.Failed("Creation", DuplicateMsg);

            // Delegate task to the general update functional handler
            return await _unitOfWork.AccountSchemeRepository.HandleUpdateAsync(_mapper, request.DTO, e => e.Id == request.DTO.Id);
        }

        private async Task<bool> IsWalletTypeExist(string walletTypeId)
        {
            var result = await _unitOfWork.AccountTypeRepository.GetAllAsync(e => e.Id == walletTypeId);
            return result.Count > 0;
        }

        private async Task<bool> IsSchemeExist(string schemename, string requestId)
        {
            var scheme = (await _unitOfWork.AccountSchemeRepository
                        .GetAllAsync(e => e.Name.ToLower() == schemename.ToLower().Trim()))
                        .FirstOrDefault();

            return scheme != null && scheme.Id != requestId;
        }
    }

}
