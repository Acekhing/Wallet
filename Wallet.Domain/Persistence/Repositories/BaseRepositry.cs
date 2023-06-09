using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Wallet.Application.Contracts.Persistence;

namespace Wallet.Infrastructure.Persistence.Repositories
{
    public class BaseRepositry<TEntity> : IBaseRepository<TEntity>
        where TEntity : class
    {
        private readonly IMongoDatabase _db;
        protected string collection { get; set; }

        public BaseRepositry(IMongoDatabase database)
        {
            _db = database;
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _db.GetCollection<TEntity>(collection).InsertOneAsync(entity);
        }

        public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> filter)
        {
            return (await _db.GetCollection<TEntity>(collection).DeleteOneAsync(filter)).IsAcknowledged;
        }

        public async Task<IList<TEntity>> GetAllAsync()
        {
            return (await _db.GetCollection<TEntity>(collection).FindAsync(_ => true)).ToList();
        }

        public async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return (await _db.GetCollection<TEntity>(collection).FindAsync(filter)).ToList();
        }

        public async Task<bool> UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>>? filter)
        {
            return (await _db.GetCollection<TEntity>(collection).ReplaceOneAsync(filter, entity)).IsAcknowledged;
        }
    }
}
