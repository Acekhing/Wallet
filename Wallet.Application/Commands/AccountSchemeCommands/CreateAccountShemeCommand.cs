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
using Wallet.Domain.Entities;

namespace Wallet.Application.Commands.AccountSchemeCommands
{
    public class CreateAccountShemeCommand : IRequest<BaseReponse>
    {
        public CreateAccountSchemeDTO DTO { get; set; }
    }

    public class CreateAccountShemeCommandHandler : IRequestHandler<CreateAccountShemeCommand, BaseReponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private const string SchemeExist = "The account scheme already exist";

        public CreateAccountShemeCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseReponse> Handle(CreateAccountShemeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseReponse();

            if (await IsWalletTypeExist(request.DTO.AccountTypeId) == false)
                return response.Failed("Creation", "Account type does not exist");

            if (await IsSchemeExist(request.DTO.Name) == true)
                return response.Failed("Creation", SchemeExist);

            // Delegate task to the general create function handler
            return await _unitOfWork.AccountSchemeRepository.HandleCreateAsync(_mapper, request.DTO);
        }

        private async Task<bool> IsWalletTypeExist(string walletTypeId)
        {
            var result = await _unitOfWork.AccountTypeRepository.GetAllAsync(e => e.Id == walletTypeId);
            return result.Count > 0;
        }

        private async Task<bool> IsSchemeExist(string schemename)
        {
            var scheme = (await _unitOfWork.AccountSchemeRepository
                        .GetAllAsync(e => e.Name.ToLower() == schemename.ToLower().Trim()))
                        .FirstOrDefault();

            return scheme != null;
        }
    }
}
