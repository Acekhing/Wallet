using FluentValidation;
using Wallet.Application.Commands.WalletTypeCommands;

namespace Wallet.Application.Validators.WalletTypeValidators
{
    public class CreateCommandValidator : AbstractValidator<CreateWalletTypeCommand>
    {
        public CreateCommandValidator()
        {
            RuleFor(e => e.Name).NotEmpty().WithMessage("The name property is required");
            RuleFor(e => e.Name).MaximumLength(50).WithMessage("Type name can not be more than 50 characters");
        }
    }
}
