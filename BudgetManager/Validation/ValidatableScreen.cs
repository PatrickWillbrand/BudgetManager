using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using FluentValidation;
using FluentValidation.Results;

namespace BudgetManager.Validation
{
    public abstract class ValidatableScreen<T> : Screen, IValidatable where T : class
    {
        private bool _canValidate;
        private AbstractValidator<T> _validator;

        public ValidatableScreen(AbstractValidator<T> validator)
        {
            _validator = validator;
            _canValidate = false;
        }

        public virtual string Error
        {
            get
            {
                IEnumerable<ValidationFailure> results = ValidateAll();
                return string.Join(Environment.NewLine, results.Select(x => x.ErrorMessage));
            }
        }

        public virtual bool IsValid
        {
            get
            {
                T instance = this as T;
                ValidationResult results = _validator.Validate(instance);
                return results.IsValid;
            }
        }

        public virtual string this[string columnName]
        {
            get
            {
                // Validierung soll beim Laden des ViewModels nicht ausgeführt werden.
                if (_canValidate)
                {
                    IEnumerable<ValidationFailure> erros = ValidateProperty(columnName);
                    ValidationFailure error = erros.FirstOrDefault();
                    if (error != null)
                    {
                        return error.ErrorMessage;
                    }
                }

                return null;
            }
        }

        public IEnumerable<ValidationFailure> ValidateAll()
        {
            T instance = this as T;
            ValidationResult results = _validator.Validate(instance);
            return results.Errors;
        }

        public IEnumerable<ValidationFailure> ValidateProperty(string propertyName)
        {
            T instance = this as T;
            ValidationResult results = _validator.Validate(instance, propertyName);
            return results.Errors;
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            _canValidate = true;
        }
    }
}