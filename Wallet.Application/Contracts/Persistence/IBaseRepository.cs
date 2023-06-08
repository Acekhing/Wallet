using System.Collections;
using System.Threading.Tasks;

namespace Wallet.Application.Contracts.Persistence
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        public Task<string> UpSert(TEntity entity);
        public Task<TEntity> GetById(string id);
        public Task<IList> GetAllAsync();
    }
}
