using FluentValidation;
using Wallet.Application.Commands.AccountSchemeCommands;

namespace Wallet.Application.Validators.AccountSchemeValidators
{
    public class DeleteCommandValidator:AbstractValidator<DeleteAccountShemeCommand>
    {
        public DeleteCommandValidator()
        {
            RuleFor(e => e.Id).NotEmpty().WithMessage("The Id property is required");
        }
    }
}
