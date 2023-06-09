using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Wallet.Application.Contracts.Persistence;
using Wallet.Application.DTOs.WalletModels;
using Wallet.Domain.Entities.WalletEntities;

namespace Wallet.Application.Queries.WalletQueries
{
    public class GetAllWalletsQuery : IRequest<IList<HubtelWallet>>
    {
    }

    public class GetAllWalletsQueryHandler : IRequestHandler<GetAllWalletsQuery, IList<HubtelWallet>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllWalletsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<HubtelWallet>> Handle(GetAllWalletsQuery request, CancellationToken cancellationToken)
        {
            var results = await _unitOfWork.WalletRepository.GetAllAsync();

            //return _mapper.Map<IList<GetWalletDto>>(results);

            return results;
        }
    }
}
