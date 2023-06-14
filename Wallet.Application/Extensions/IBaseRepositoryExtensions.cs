using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Wallet.Application.Contracts.Persistence;
using Wallet.Application.Exceptions;
using Wallet.Application.Responses;

namespace Wallet.Application.Extensions
{
    public static class IBaseRepositoryExtensions
    {
        public static async Task<BaseReponse> HandleCreateAsync<TEntity, DTO>(
            this IBaseRepository<TEntity> repository, 
            IMapper mapper, 
            DTO payload) where TEntity : class where DTO : class
        {
            var response = new BaseReponse();

            var entity = mapper.Map<TEntity>(payload);

            await repository.CreateAsync(entity);

            return response.Success(message:"Record created successfully");
        }

        public static async Task<BaseReponse> HandleUpdateAsync<TEntity, DTO>(
            this IBaseRepository<TEntity> repository, 
            IMapper mapper, 
            DTO payload,
            Expression<Func<TEntity,bool>> predicate
            ) where TEntity : class where DTO : class
        {
            var response = new BaseReponse();

            var existingRecords = await repository.GetAllAsync(predicate);

            if (existingRecords.Count == 0)
                throw new EntityNotFoundException("The record you are trying to update does not exist");

            var entity = mapper.Map(payload, existingRecords.First());

            var result = await repository.UpdateAsync(entity, predicate);

            if (result == true)
                return response.Success(message:"Record updated successfully");

            return response.Failed("Update", "Could not update record");
        }

        public static async Task<BaseReponse> HandleDeleteAsync<TEntity>(
            this IBaseRepository<TEntity> repository,
            Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            var response = new BaseReponse();

            var existingRecords = await repository.GetAllAsync(predicate);

            if (existingRecords.Count == 0)
                throw new EntityNotFoundException("The record you are trying to delete does not exist");

            var result = await repository.DeleteAsync(predicate);

            if (result == true)
                return response.Success(message:"Record created successfully");

            return response.Failed("Delete", "Could not delete record");
        }
    }
}
