using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Wallet.Application.Contracts.Persistence;
using Wallet.Application.Responses;

namespace Wallet.Application.Queries.AccountSchemeQueries
{
    public class GetAllAccountSchemesQuery: IRequest<QueryResponse> { }

    public class GetAllAccountSchemesQueryHandler: IRequestHandler<GetAllAccountSchemesQuery, QueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAccountSchemesQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<QueryResponse> Handle(GetAllAccountSchemesQuery request, CancellationToken cancellationToken)
        {
            var results = await _unitOfWork.AccountSchemeRepository.GetAllAsync(_ => true);
            return new QueryResponse { Data = results, Success = true };
        }
    }
}
