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
        public string? Search { get; set; } = null;
    }

    public class GetAllAccountSchemesQueryHandler: IRequestHandler<GetAllAccountSchemesQuery, IList<AccountScheme>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAccountSchemesQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<IList<AccountScheme>> Handle(GetAllAccountSchemesQuery request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.Search.Trim()))
            {
                return await _unitOfWork.AccountSchemeRepository
                            .GetAllAsync(e => e.Name.ToLower() == request.Search.ToLower());
            }

            return await _unitOfWork.AccountSchemeRepository.GetAllAsync(_ => true);
        }
    }
}
