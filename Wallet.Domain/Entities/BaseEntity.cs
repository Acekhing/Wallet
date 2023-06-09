using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Wallet.Domain.Entities
{
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public DateTime EditedAt { get; set; } = DateTime.Now;
    }
}
