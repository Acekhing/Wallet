using FluentValidation;
using Wallet.Application.Commands.AccountSchemeCommands;

namespace Wallet.Application.Validators.AccountSchemeValidators
{
    public class CreateCommandValidator : AbstractValidator<CreateAccountShemeCommand>
    {
        public CreateCommandValidator()
        {
            RuleFor(e => e.DTO.Name).NotEmpty().WithMessage("Account scheme name is required");
            RuleFor(e => e.DTO.Name).MaximumLength(50).WithMessage("Scheme name can not be more than 50 characters");
            RuleFor(e => e.DTO.AccountTypeId).NotEmpty().WithMessage("Account type id required");
        }
    }
}
