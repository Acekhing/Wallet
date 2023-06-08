using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Wallet.Application.Contracts.Persistence;
using Wallet.Application.Extensions;
using Wallet.Application.Responses;

namespace Wallet.Application.Commands.AccountSchemeCommands
{
    public class UpdateeAccountShemeCommand : IRequest<BaseReponse>
    {
        public string Id { get; private set; }
        public string Name { get; set; }
        public DateTime EditedAt { get; set; }
        public string WalletTypeId { get; set; }
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

            var scheme = await _unitOfWork.AccountSchemeRepository.GetByNameAsync(request.Name);

            if (scheme != null && scheme.Id != request.Id) // Checking if wallet type already exist. 
                return response.Failed("Update", DuplicateMsg);

            return await _unitOfWork.AccountSchemeRepository.HandleUpdateAsync(_mapper, request, request.Id);
        }
    }

}
