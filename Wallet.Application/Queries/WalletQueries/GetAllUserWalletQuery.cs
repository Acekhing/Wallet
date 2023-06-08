using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Wallet.Application.Contracts.Persistence;
using Wallet.Application.DTOs.WalletModels;

namespace Wallet.Application.Queries.WalletQueries
{
    public class GetAllUserWalletQuery : IRequest<IList<GetWalletDto>>
    {
    }

    public class GetAllUserWalletQueryHandler : IRequestHandler<GetAllUserWalletQuery, IList<GetWalletDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllUserWalletQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<GetWalletDto>> Handle(GetAllUserWalletQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.WalletRepository.GetAllAsync();
            return _mapper.Map<IList<GetWalletDto>>(result);
        }
    }
}
