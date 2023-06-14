using FluentValidation;
using Wallet.Application.Commands.WalletTypeCommands;

namespace Wallet.Application.Validators.WalletTypeValidators
{
    public class UpdateCommandValidator : AbstractValidator<UpdateAccountTypeCommand>
    {
        public UpdateCommandValidator()
        {
            RuleFor(e => e.DTO.Id).NotEmpty().WithMessage("Account type Id is required");
            RuleFor(e => e.DTO.Name).NotEmpty().WithMessage("Account type name is required");
            RuleFor(e => e.DTO.Code).NotEmpty().WithMessage("Account type code is required");
            RuleFor(e => e.DTO.Name).MaximumLength(50).WithMessage("Account type name can not be more than 50 characters");
        }
    }
}
