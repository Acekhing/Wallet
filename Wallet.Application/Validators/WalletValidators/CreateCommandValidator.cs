using FluentValidation;
using Wallet.Application.Commands.WalletCommands;

namespace Wallet.Application.Validators.WalletValidators
{
    public class CreateCommandValidator: AbstractValidator<CreateWalletCommand>
    {
        public CreateCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("The name property is required");
            RuleFor(e => e.Name).MaximumLength(150).WithMessage("Wallet name can not be more than 50 characters");
            RuleFor(c => c.UserId).NotEmpty().WithMessage("The user id property is required");
            RuleFor(c => c.WalletTypeId).NotEmpty().WithMessage("The wallet type id is required");
            RuleFor(c => c.AccountSchemeId).NotEmpty().WithMessage("The account schema is required");
            RuleFor(c => c.AccountNumber).NotEmpty().WithMessage("The account number is required");
            RuleFor(c => c.AccountNumber).MinimumLength(10).WithMessage("Account number cannot be less than 10");
        }
    }
}
