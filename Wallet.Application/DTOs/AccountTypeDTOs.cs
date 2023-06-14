using System;

namespace Wallet.Application.DTOs
{
    public class CreateAccountTypeDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public bool Active { get; set; } = true;
    }

    public class UpdateAccountTypeDTO
    {
        public string Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public bool Active { get; set; } = true;
        public DateTime EditedAt { get; set; } = DateTime.Now;
    }

    public class GetAccountTypeDTO
    {
        public string Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public bool Active { get; set; } = true;
    }
}
