﻿namespace Wallet.Domain.Entities.WalletEntities
{
    public class HubtelWallet: BaseEntity
    {
        public string UserId { get; set; }
        public string WalletTypeId { get; set; }
        public string AccountSchemeId { get; set; }
    }
}