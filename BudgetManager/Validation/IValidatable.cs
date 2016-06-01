using System.Collections.Generic;
using System.ComponentModel;
using FluentValidation.Results;

namespace BudgetManager.Validation
{
    public interface IValidatable : IDataErrorInfo
    {
        bool IsValid { get; }

        IEnumerable<ValidationFailure> ValidateAll();

        IEnumerable<ValidationFailure> ValidateProperty(string propertyName);
    }
}