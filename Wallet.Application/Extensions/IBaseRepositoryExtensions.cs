using AutoMapper;
using System.Threading.Tasks;
using Wallet.Application.Contracts.Persistence;
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

            var id = await repository.UpSert(entity); // Create record

            if (!string.IsNullOrEmpty(id))
                return response.Created(id, $"{nameof(TEntity)} created successfully");

            return response.Failed("Action failed");
        }

        public static async Task<BaseReponse> HandleUpdateAsync<TEntity, TPayload>(this IBaseRepository<TEntity> repository, IMapper mapper, TPayload payload, string id)
            where TEntity : class where TPayload : class
        {
            var response = new BaseReponse(); // Create a response object

            // Check if record exist
            var existingRecord = await repository.GetById(id);

            if (existingRecord == null)
                throw new System.Exception();

            var entity = mapper.Map(payload,existingRecord); // Map payload to domain entity

            id = await repository.UpSert(entity); // Create record

            if (!string.IsNullOrEmpty(id))
                return response.Created(id, $"{nameof(TEntity)} created successfully");

            return response.Failed("Action failed");
        }
    }
}
