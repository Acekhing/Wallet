using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wallet.Application.Contracts.Persistence
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        public Task<string> CreateAsync(TEntity entity);
        public Task<string> UpdateAsync(TEntity entity);
        public Task<bool> DeleteAsync(string id);
        public Task<TEntity> GetByIdAsync(string id);
        public Task<TEntity> GetByNameAsync(string name);
        public Task<IList<TEntity>> GetAllAsync();
    }
}
