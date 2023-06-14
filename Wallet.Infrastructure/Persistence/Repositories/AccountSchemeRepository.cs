using MongoDB.Driver;
using Wallet.Application.Contracts.Persistence;
using Wallet.Domain.Entities;

namespace Wallet.Infrastructure.Persistence.Repositories
{
    public class AccountSchemeRepository : BaseRepositry<AccountScheme>, IAccountSchemeRepository
    {
        public AccountSchemeRepository(IMongoDatabase database) : base(database)
        {
            collection = "account_scheme";
        }
    }
}
