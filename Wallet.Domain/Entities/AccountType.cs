namespace Wallet.Domain.Entities
{
    public class AccountType : BaseEntity
    {
        public string Code { get; set; }
        public bool Active { get; set; }
    }
}
