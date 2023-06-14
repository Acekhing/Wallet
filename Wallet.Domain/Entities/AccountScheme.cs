namespace Wallet.Domain.Entities
{
    public class AccountScheme : BaseEntity
    {
        public string AccountTypeId { get; set; } = string.Empty; 
        public string AccountTypeName { get; set; } = string.Empty;
        public bool Active { get; set; }
    }
}
