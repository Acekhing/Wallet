using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Wallet.Application.Contracts.Persistence;
using Wallet.Application.DTOs.WalletModels;

namespace Wallet.Application.Queries.WalletQueries
{
    public class GetAllWalletsQuery : IRequest<IList<GetWalletDto>>
    {
    }

    public class GetAllWalletsQueryHandler : IRequestHandler<GetAllWalletsQuery, IList<GetWalletDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllWalletsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<GetWalletDto>> Handle(GetAllWalletsQuery request, CancellationToken cancellationToken)
        {
            var results = await _unitOfWork.WalletRepository.GetAllAsync();

            return _mapper.Map<IList<GetWalletDto>>(results);
        }
    }
}
