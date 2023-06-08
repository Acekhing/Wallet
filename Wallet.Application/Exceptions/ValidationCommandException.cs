using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace Wallet.Application.Exceptions
{
    public class ValidationCommandException : ApplicationException
    {
        public IDictionary<string, string[]> Errors { get; }
        public ValidationCommandException() : base("One or more validation failures occurred")
        {

        }

        public ValidationCommandException(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(f => f.Key, f => f.ToArray());

        }
    }
}
