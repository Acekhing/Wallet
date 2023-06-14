using FluentValidation;
using Wallet.Application.Commands.WalletTypeCommands;

namespace Wallet.Application.Validators.WalletTypeValidators
{
    public class CreateCommandValidator : AbstractValidator<CreateAccountTypeCommand>
    {
        public CreateCommandValidator()
        {
            RuleFor(e => e.DTO.Name).NotEmpty().WithMessage("Account type name is required");
            RuleFor(e => e.DTO.Code).NotEmpty().WithMessage("Account type code is required");
            RuleFor(e => e.DTO.Name).MaximumLength(50).WithMessage("Account type name can not be more than 50 characters");
        }
    }
}
