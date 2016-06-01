using System.Collections.Generic;
using FluentValidation.Results;

namespace BudgetManager.Domain.Validators
{
    public interface IValidatable
    {
        bool IsValid { get; }

        IEnumerable<ValidationFailure> ValidateAll();

        IEnumerable<ValidationFailure> ValidateProperty(string propertyName);
    }
}