using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Wallet.Application.Contracts.Persistence;
using Wallet.Application.Extensions;
using Wallet.Application.Responses;

namespace Wallet.Application.Commands.AccountSchemeCommands
{
    public class CreateAccountShemeCommand : IRequest<BaseReponse>
    {
        public string Name { get; set; }
        public string WalletTypeId { get; set; }
    }

    public class CreateAccountShemeCommandHandler : IRequestHandler<CreateAccountShemeCommand, BaseReponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private const string DuplicateMsg = "The account scheme exist for another record";

        public CreateAccountShemeCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseReponse> Handle(CreateAccountShemeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseReponse();

            // Check if account scheme does not exist
            var scheme = await _unitOfWork.AccountSchemeRepository.GetByNameAsync(request.Name);

            if (scheme != null) // Checking if wallet type already exist. 
                return response.Failed("Creation", DuplicateMsg);

            // Delegate task to the general create execution
            return await _unitOfWork.AccountSchemeRepository.HandleCreateAsync(_mapper, request);
        }
    }
}
