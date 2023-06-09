using FluentValidation;
using Wallet.Application.Commands.WalletTypeCommands;

namespace Wallet.Application.Validators.WalletTypeValidators
{
    public class DeleteCommandValidator: AbstractValidator<DeleteWalletTypeCommand>
    {
        public DeleteCommandValidator()
        {
            RuleFor(e => e.Id).NotEmpty().WithMessage("The Id property is required");
        }
    }
}
