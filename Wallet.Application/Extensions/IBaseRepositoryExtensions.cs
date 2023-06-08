using System.Threading.Tasks;
using AutoMapper;
using Wallet.Application.Contracts.Persistence;
using Wallet.Application.Exceptions;
using Wallet.Application.Responses;

namespace Wallet.Application.Extensions
{
    public static class IBaseRepositoryExtensions
    {
        public static async Task<BaseReponse> HandleCreateAsync<TEntity, TPayload>(this IBaseRepository<TEntity> repository, IMapper mapper, TPayload payload)
            where TEntity : class where TPayload : class
        {
            var response = new BaseReponse(); // Create a response object

            var entity = mapper.Map<TEntity>(payload); // Map payload to domain entity

            var id = await repository.UpSertAsync(entity); // Create record

            if (!string.IsNullOrEmpty(id))
                return response.Created(id, $"{nameof(TEntity)} created successfully");

            return response.Failed("Create", "Could not create record");
        }

        public static async Task<BaseReponse> HandleUpdateAsync<TEntity, TPayload>(this IBaseRepository<TEntity> repository, IMapper mapper, TPayload payload, string id)
            where TEntity : class where TPayload : class
        {
            var response = new BaseReponse(); // Create a response object

            // Check if record exist
            var existingRecord = await repository.GetByIdAsync(id);

            if (existingRecord == null)
                throw new EntityNotFoundException("The record you are trying to update does not exist");

            var entity = mapper.Map(payload, existingRecord); // Map payload to domain entity

            id = await repository.UpSertAsync(entity); // Create record

            if (!string.IsNullOrEmpty(id))
                return response.Created(id, $"{nameof(TEntity)} created successfully");

            return response.Failed("Update", "Could not update record");
        }
    }
}
