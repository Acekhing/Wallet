using AutoMapper;
using MediatR;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wallet.Application.Contracts.Persistence;
using Wallet.Application.DTOs;
using Wallet.Application.Responses;

namespace Wallet.Application.Queries.WalletTypeQueries
{
    public class GetAllWalletTypeQuery: IRequest<QueryResponse> { } 

    public class GetAllWalletTypeQueryHandler: IRequestHandler<GetAllWalletTypeQuery, QueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllWalletTypeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<QueryResponse> Handle(GetAllWalletTypeQuery request, CancellationToken cancellationToken)
        {
            var results = await _unitOfWork.AccountTypeRepository.GetAllAsync(_ => true);
            var mappedResults = _mapper.Map<List<GetAccountTypeDTO>>(results.ToList());

            return new QueryResponse { Data = results, Success = true };
        }
    }
}
