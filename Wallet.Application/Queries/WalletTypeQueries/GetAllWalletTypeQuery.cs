using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Wallet.Application.Contracts.Persistence;
using Wallet.Application.Responses;

namespace Wallet.Application.Queries.WalletTypeQueries
{
    public class GetAllWalletTypeQuery: IRequest<QueryResponse> { } 

    public class GetAllWalletTypeQueryHandler: IRequestHandler<GetAllWalletTypeQuery, QueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllWalletTypeQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<QueryResponse> Handle(GetAllWalletTypeQuery request, CancellationToken cancellationToken)
        {
            var results = await _unitOfWork.WalletTypeRepository.GetAllAsync(_ => true);
            return new QueryResponse { Data = results, Success = true };
        }
    }
}
