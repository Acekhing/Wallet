using FluentValidation;
using Wallet.Application.Commands.AccountSchemeCommands;

namespace Wallet.Application.Validators.AccountSchemeValidators
{
    public class CreateCommandValidator : AbstractValidator<CreateAccountShemeCommand>
    {
        public CreateCommandValidator()
        {
            RuleFor(e => e.Name).NotEmpty().WithMessage("Please enter scheme name");
            RuleFor(e => e.Name).MaximumLength(50).WithMessage("Scheme name can not be more than 50 characters");
            RuleFor(e => e.WalletTypeId).NotEmpty().WithMessage("Please provide wallet type for this scheme");
        }
    }
}
