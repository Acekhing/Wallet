using FluentValidation;
using Wallet.Application.Commands.AccountSchemeCommands;

namespace Wallet.Application.Validators.AccountSchemeValidators
{
    public class UpdateCommandValidator : AbstractValidator<UpdateeAccountShemeCommand>
    {
        public UpdateCommandValidator()
        {
            RuleFor(e => e.DTO.Id).NotEmpty().WithMessage("Account scheme Id is required");
            RuleFor(e => e.DTO.Name).NotEmpty().WithMessage("Account scheme name is required");
            RuleFor(e => e.DTO.Name).MaximumLength(50).WithMessage("Scheme name can not be more than 50 characters");
            RuleFor(e => e.DTO.AccountTypeId).NotEmpty().WithMessage("Account type id required");
        }
    }
}
