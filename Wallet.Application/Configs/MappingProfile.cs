using AutoMapper;
using Wallet.Application.Commands.AccountSchemeCommands;
using Wallet.Application.Commands.WalletCommands;
using Wallet.Application.DTOs;
using Wallet.Application.DTOs.WalletDtos;
using Wallet.Domain.Entities;

namespace Wallet.Application.Configs
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<AccountType, CreateAccountTypeDTO>().ReverseMap();
            CreateMap<AccountType, UpdateAccountTypeDTO>().ReverseMap();
            CreateMap<AccountType, GetAccountTypeDTO>().ReverseMap();
            CreateMap<HubtelWallet, CreateWalletCommand>().ReverseMap();
            CreateMap<HubtelWallet, UpdateWalletCommand>().ReverseMap();
            CreateMap<HubtelWallet, GetWalletDto>().ReverseMap();
            CreateMap<AccountScheme, CreateAccountShemeCommand>().ReverseMap();
            CreateMap<AccountScheme, UpdateeAccountShemeCommand>().ReverseMap();
            CreateMap<AccountScheme, GetAccountSchemeDto>().ReverseMap();
        }
    }
}
