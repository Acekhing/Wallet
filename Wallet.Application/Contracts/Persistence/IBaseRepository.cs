using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wallet.Application.Contracts.Persistence
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        public Task<string> UpSertAsync(TEntity entity);
        public Task<TEntity> GetByIdAsync(string id);
        public Task<TEntity> GetByNameAsync(string name);
        public Task<IList<TEntity>> GetAllAsync();
    }
}
