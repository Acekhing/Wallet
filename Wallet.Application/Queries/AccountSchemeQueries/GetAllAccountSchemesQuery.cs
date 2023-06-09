using AutoMapper;
using MediatR;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Wallet.Application.Contracts.Persistence;
using Wallet.Application.DTOs.WalletModels;
using Wallet.Domain.Entities.WalletEntities;

namespace Wallet.Application.Queries.AccountSchemeQueries
{
    public class GetAllAccountSchemesQuery: IRequest<IList<AccountScheme>>
    {
    }

    public class GetAllAccountSchemesQueryHandler: IRequestHandler<GetAllAccountSchemesQuery, IList<AccountScheme>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllAccountSchemesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<AccountScheme>> Handle(GetAllAccountSchemesQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.AccountSchemeRepository.GetAllAsync(_ => true);
        }
    }
}
