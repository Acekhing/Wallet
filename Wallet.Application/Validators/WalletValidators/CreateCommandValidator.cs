using FluentValidation;
using Wallet.Application.Commands.WalletCommands;

namespace Wallet.Application.Validators.WalletValidators
{
    public class CreateCommandValidator: AbstractValidator<CreateWalletCommand>
    {
        public CreateCommandValidator()
        {
            RuleFor(c => c.DTO.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(c => c.DTO.UserId).NotEmpty().WithMessage("User id is required");
            RuleFor(c => c.DTO.AccountTypeId).NotEmpty().WithMessage("Account type id is required");
            RuleFor(c => c.DTO.AccountSchemeId).NotEmpty().WithMessage("Account schema is required");
            RuleFor(c => c.DTO.AccountNumber).NotEmpty().WithMessage("Account number is required");
            RuleFor(c => c.DTO.AccountNumber).MinimumLength(10).WithMessage("Account number cannot be less than 10");
        }
    }
}
