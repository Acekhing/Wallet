namespace Wallet.Domain.Entities.WalletEntities
{
    public class HubtelWallet : BaseEntity
    {
        public string UserId { get; set; }
        public string WalletTypeId { get; set; }
        public string AccountSchemeId { get; set; }
        public string AccountNumber { get; set; }
        public string Owner { get; set; }
        public string EncryptedAccountNumber { private get; set; }
    }
}
