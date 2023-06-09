using FluentValidation;
using Wallet.Application.Commands.AuthCommands;

namespace Wallet.Application.Validators.AuthValidators
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(e => e.LoginDto.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(e => e.LoginDto.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}
