using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wallet.Application.Contracts.Persistence;
using Wallet.Domain.Entities.WalletEntities;

namespace Wallet.Application.Queries.WalletQueries
{
    public class GetAllWalletsQuery : IRequest<IList<HubtelWallet>>
    {
    }

    public class GetAllWalletsQueryHandler : IRequestHandler<GetAllWalletsQuery, IList<HubtelWallet>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;
        private const string cachepath = "wallets/all";

        public GetAllWalletsQueryHandler(IUnitOfWork unitOfWork, ICacheService cacheService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
            _mapper = mapper;
        }

        public async Task<IList<HubtelWallet>> Handle(GetAllWalletsQuery request, CancellationToken cancellationToken)
        {
            // Check cache data
            var cachedata = _cacheService.GetData<List<HubtelWallet>>(cachepath);
            if (cachedata != null && cachedata.Count > 0)
                return cachedata;

            cachedata = (await _unitOfWork.WalletRepository.GetAllAsync()).ToList();

            if (cachedata.Count > 0)
            {
                var expiryTime = DateTimeOffset.Now.AddSeconds(50); // expiry time
                _cacheService.SetData(cachepath, cachedata, expiryTime); // set cached data
            }

            return cachedata;
        }
    }
}
