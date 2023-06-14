using System;

namespace Wallet.Application.DTOs
{
    public class CreateWalletDTO
    {
        public string Name { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string AccountTypeId { get; set; } = string.Empty;
        public string AccountSchemeId { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
    }

    public class UpdateWalletDTO : CreateWalletDTO
    {
        public string Id { get; set; } = string.Empty;
        public DateTime EditedAt { get; set; }
    }
    public class GetWalletDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string AccountScheme { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
    }
}
