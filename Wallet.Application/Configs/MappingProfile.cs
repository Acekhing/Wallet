using AutoMapper;
using Wallet.Application.DTOs;
using Wallet.Domain.Entities;

namespace Wallet.Application.Configs
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            // Profiles for account type
            CreateMap<AccountType, CreateAccountTypeDTO>().ReverseMap();
            CreateMap<AccountType, UpdateAccountTypeDTO>().ReverseMap();
            CreateMap<AccountType, GetAccountTypeDTO>().ReverseMap();

            // Profiles for wallet
            CreateMap<HubtelWallet, CreateWalletDTO>().ReverseMap();
            CreateMap<HubtelWallet, UpdateWalletDTO>().ReverseMap();
            CreateMap<HubtelWallet, GetWalletDTO>().ReverseMap();
            CreateMap<HubtelWallet, HubtelWallet>().ReverseMap();

            // Profiles for account scheme
            CreateMap<AccountScheme, CreateAccountSchemeDTO>().ReverseMap();
            CreateMap<AccountScheme, UpdateAccountSchemeDTO>().ReverseMap();
            CreateMap<AccountScheme, GetAccountSchemeDTO>().ReverseMap();
        }
    }
}
