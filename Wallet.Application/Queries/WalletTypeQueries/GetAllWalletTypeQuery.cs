using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Wallet.Application.Contracts.Persistence;
using Wallet.Application.DTOs.WalletModels;
using Wallet.Domain.Entities.WalletEntities;

namespace Wallet.Application.Queries.WalletTypeQueries
{
    public class GetAllWalletTypeQuery: IRequest<IList<WalletType>>
    {

    } 

    public class GetAllWalletTypeQueryHandler: IRequestHandler<GetAllWalletTypeQuery, IList<WalletType>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllWalletTypeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<WalletType>> Handle(GetAllWalletTypeQuery request, CancellationToken cancellationToken)
        {
            var results = await _unitOfWork.WalletTypeRepository.GetAllAsync(_ => true);
            
            //return _mapper.Map<IList<GetWalletTypeDto>>(results);

            return results;
        }
    }
}
