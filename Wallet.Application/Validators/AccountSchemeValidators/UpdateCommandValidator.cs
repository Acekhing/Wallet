﻿using FluentValidation;
using Wallet.Application.Commands.AccountSchemeCommands;

namespace Wallet.Application.Validators.AccountSchemeValidators
{
    public class UpdateCommandValidator: AbstractValidator<UpdateeAccountShemeCommand>
    {
        public UpdateCommandValidator()
        {
            RuleFor(e => e.Id).NotEmpty().WithMessage("The Id property is required");
            RuleFor(e => e.Name).NotEmpty().WithMessage("Please enter scheme name");
            RuleFor(e => e.WalletTypeId).NotEmpty().WithMessage("Please provide wallet type for this scheme");
        }
    }
}