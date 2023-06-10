using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Wallet.Application.Contracts.Persistence;
using Wallet.Domain.Entities.WalletEntities;

namespace Wallet.Application.Queries.WalletTypeQueries
{
    public class GetAllWalletTypeQuery: IRequest<IList<WalletType>>
    {
        public string? Search { get; set; } = null;
    } 

    public class GetAllWalletTypeQueryHandler: IRequestHandler<GetAllWalletTypeQuery, IList<WalletType>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllWalletTypeQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<IList<WalletType>> Handle(GetAllWalletTypeQuery request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.Search.Trim()))
            {
                return await _unitOfWork.WalletTypeRepository
                            .GetAllAsync(e => e.Name.ToLower() == request.Search.ToLower());
            }

            var results = await _unitOfWork.WalletTypeRepository.GetAllAsync(_ => true);

            return await _unitOfWork.WalletTypeRepository.GetAllAsync(_ => true);
        }
    }
}
