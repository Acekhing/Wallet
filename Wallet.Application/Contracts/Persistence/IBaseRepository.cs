using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Wallet.Application.Contracts.Persistence
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        public Task CreateAsync(TEntity entity);
        public Task<bool> UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> filter);
        public Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> filter);
        public Task<IList<TEntity>> GetAllAsync();
        public Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null);
    }
}
