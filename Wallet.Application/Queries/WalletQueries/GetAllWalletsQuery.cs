using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wallet.Application.Contracts.Persistence;
using Wallet.Application.Responses;
using Wallet.Application.Utilities;
using Wallet.Domain.Entities;

namespace Wallet.Application.Queries.WalletQueries
{
    public class GetAllWalletsQuery : IRequest<QueryResponse>
    {
    }

    public class GetAllWalletsQueryHandler : IRequestHandler<GetAllWalletsQuery, QueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;
        private const string cachekey = "wallets/all";

        public GetAllWalletsQueryHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }

        public async Task<QueryResponse> Handle(GetAllWalletsQuery request, CancellationToken cancellationToken)
        {
            // Check cache data
            var cachedata = _cacheService.GetData<List<HubtelWallet>>(cachekey);
            if (cachedata != null && cachedata.Count > 0)
                return new QueryResponse { Data = cachedata, Success = true };

            // Get data from database
            cachedata = (await _unitOfWork.WalletRepository.GetAllAsync()).ToList();

            if (cachedata.Count > 0) // cache data
                _cacheService.SetCacheData(cachekey, cachedata);

            return new QueryResponse { Data = cachedata, Success = true };
        }
    }
}
