using System;

namespace Wallet.Application.DTOs.WalletDtos
{
    public class GetWalletDto
    {
        public string Id { get; private set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string WalletTypeId { get; set; } = string.Empty;
        public string AccountSchemeId { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public DateTime CreatedAt { get; private set; }
        public DateTime EditedAt { get; set; }
    }
}
