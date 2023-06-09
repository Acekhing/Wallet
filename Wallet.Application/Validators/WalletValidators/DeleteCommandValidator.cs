using FluentValidation;
using Wallet.Application.Commands.WalletCommands;

namespace Wallet.Application.Validators.WalletValidators
{
    public class DeleteCommandValidator: AbstractValidator<DeleteWalletCommand>
    {
        public DeleteCommandValidator()
        {
            RuleFor(e => e.Id).NotEmpty().WithMessage("The Id property id required");
        }
    }
}
