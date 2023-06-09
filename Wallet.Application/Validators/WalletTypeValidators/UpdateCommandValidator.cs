using FluentValidation;
using Wallet.Application.Commands.WalletTypeCommands;

namespace Wallet.Application.Validators.WalletTypeValidators
{
    public class UpdateCommandValidator: AbstractValidator<UpdateWalletTypeCommand>
    {
        public UpdateCommandValidator()
        {
            RuleFor(e => e.Id).NotEmpty().WithMessage("The Id property is required");
            RuleFor(e => e.Name).NotEmpty().WithMessage("The name property is required");
        }
    }
}
