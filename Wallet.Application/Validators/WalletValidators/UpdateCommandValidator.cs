using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Wallet.Application.Commands.WalletCommands;

namespace Wallet.Application.Validators.WalletValidators
{
    public class UpdateCommandValidator: AbstractValidator<UpdateWalletCommand>
    {
        public UpdateCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("The name property is required");
            RuleFor(c => c.UserId).NotEmpty().WithMessage("The user id property is required");
            RuleFor(c => c.WalletTypeId).NotEmpty().WithMessage("The wallet type id is required");
            RuleFor(c => c.AccountSchemeId).NotEmpty().WithMessage("The account schema is required");
            RuleFor(c => c.AccountNumber).NotEmpty().WithMessage("The account number is required");
            RuleFor(c => c.EditedAt).NotEmpty().WithMessage("The edited date is required");
        }
    }
}
