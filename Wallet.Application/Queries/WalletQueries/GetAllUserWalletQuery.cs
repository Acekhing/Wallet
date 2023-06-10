using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wallet.Application.Contracts.Persistence;
using Wallet.Application.Utilities;
using Wallet.Domain.Entities.WalletEntities;

namespace Wallet.Application.Queries.WalletQueries
{
    public class GetAllUserWalletQuery : IRequest<IList<HubtelWallet>>
    {
        public string UserId { get; set; }
    }

    public class GetAllUserWalletQueryHandler : IRequestHandler<GetAllUserWalletQuery, IList<HubtelWallet>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;
        private const string cachekey = "wallets/user";

        public GetAllUserWalletQueryHandler(IUnitOfWork unitOfWork, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }

        public async Task<IList<HubtelWallet>> Handle(GetAllUserWalletQuery request, CancellationToken cancellationToken)
        {
            // Check cache data
            var cachedata = _cacheService.GetData<List<HubtelWallet>>(cachekey);
            if (cachedata != null && cachedata.Count > 0)
                return cachedata;

            // Get data from database
            cachedata = (await _unitOfWork.WalletRepository
                        .GetAllAsync(e => e.UserId == request.UserId)).ToList();

            if (cachedata.Count > 0) // cache data
                _cacheService.SetCacheData(cachekey, cachedata);

            return cachedata;
        }
    }
}
