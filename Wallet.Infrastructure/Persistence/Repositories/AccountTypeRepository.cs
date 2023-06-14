using MongoDB.Driver;
using Wallet.Application.Contracts.Persistence;
using Wallet.Domain.Entities;

namespace Wallet.Infrastructure.Persistence.Repositories
{
    public class AccountTypeRepository : BaseRepositry<AccountType>, IAccountTypeRepository
    {
        public AccountTypeRepository(IMongoDatabase database) : base(database)
        {
            collection = "wallet_type";
        }
    }
}
