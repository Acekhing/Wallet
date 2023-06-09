using AutoMapper;
using Wallet.Application.Commands.AccountSchemeCommands;
using Wallet.Application.Commands.WalletCommands;
using Wallet.Application.Commands.WalletTypeCommands;
using Wallet.Application.DTOs.WalletModels;
using Wallet.Domain.Entities.WalletEntities;

namespace Wallet.Application.Configs
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<WalletType, CreateWalletTypeCommand>().ReverseMap();
            CreateMap<WalletType, UpdateWalletTypeCommand>().ReverseMap();
            CreateMap<WalletType, GetWalletTypeDto>().ReverseMap();
            CreateMap<HubtelWallet, CreateWalletCommand>().ReverseMap();
            CreateMap<HubtelWallet, UpdateWalletCommand>().ReverseMap();
            CreateMap<HubtelWallet, GetWalletDto>().ReverseMap();
            CreateMap<AccountScheme, CreateAccountShemeCommand>().ReverseMap();
            CreateMap<AccountScheme, UpdateeAccountShemeCommand>().ReverseMap();
            CreateMap<AccountScheme, GetAccountSchemeDto>().ReverseMap();
        }
    }
}
