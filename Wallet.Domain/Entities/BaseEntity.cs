using System;

namespace Wallet.Domain.Entities
{
    public class BaseEntity
    {
        public string Id { get; private set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public DateTime EditedAt { get; set; } = DateTime.Now;
    }
}
