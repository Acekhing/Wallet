using AutoMapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wallet.Application.Contracts.Persistence;
using Wallet.Application.DTOs.WalletModels;
using Wallet.Domain.Entities.WalletEntities;

namespace Wallet.Application.Queries.WalletQueries
{
    public class GetWalletByIdQuery: IRequest<HubtelWallet>
    {
        public string UserId { get; set; }
        public string WalletId { get; set; }
    }

    public class GetWalletByIdQueryHandler: IRequestHandler<GetWalletByIdQuery, HubtelWallet>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetWalletByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<HubtelWallet> Handle(GetWalletByIdQuery request, CancellationToken cancellationToken)
        {
            var result = (await _unitOfWork.WalletRepository
                            .GetAllAsync(e => e.Id == request.WalletId && e.UserId == request.UserId))
                            .FirstOrDefault();

            return result;
        }
    }
}
