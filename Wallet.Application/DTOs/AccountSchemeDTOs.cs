using System;

namespace Wallet.Application.DTOs
{
    public class CreateAccountSchemeDTO
    {
        public string Name { get; set; } = string.Empty;
        public string AccountTypeId { get; set; } = string.Empty;
        public bool Active { get; set; }
    }

    public class UpdateAccountSchemeDTO : CreateAccountSchemeDTO
    {
        public string Id { get; set; }
        public DateTime EditedAt { get; set; } = DateTime.Now;
    }

    public class GetAccountSchemeDTO : CreateAccountSchemeDTO
    {
        public string Id { get; set; }
    }
}
