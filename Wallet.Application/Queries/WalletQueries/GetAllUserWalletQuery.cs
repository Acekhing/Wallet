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
    public class GetAllUserWalletQuery : IRequest<IList<HubtelWallet>>
    {
        public string UserId { get; set; }
    }

    public class GetAllUserWalletQueryHandler : IRequestHandler<GetAllUserWalletQuery, IList<HubtelWallet>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllUserWalletQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<HubtelWallet>> Handle(GetAllUserWalletQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.WalletRepository.GetAllAsync(e => e.UserId == request.UserId);
            //return _mapper.Map<IList<GetWalletDto>>(result);
            return result;
        }
    }
}
