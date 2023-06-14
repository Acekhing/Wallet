namespace Wallet.Domain.Entities
{
    public class HubtelWallet : BaseEntity
    {
        public string UserId { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string AccountScheme { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public string EncryptedAccountNumber { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
    }
}
