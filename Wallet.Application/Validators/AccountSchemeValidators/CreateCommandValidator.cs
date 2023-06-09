using FluentValidation;
using Wallet.Application.Commands.AccountSchemeCommands;

namespace Wallet.Application.Validators.AccountSchemeValidators
{
    public class CreateCommandValidator: AbstractValidator<CreateAccountShemeCommand>
    {
        public CreateCommandValidator()
        {
            RuleFor(e => e.Name).NotEmpty().WithMessage("Please enter scheme name");
            RuleFor(e => e.WalletTypeId).NotEmpty().WithMessage("Please provide wallet type for this scheme");
        }
    }
}
