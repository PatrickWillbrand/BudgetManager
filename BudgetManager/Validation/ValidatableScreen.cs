using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using FluentValidation;
using FluentValidation.Results;

namespace BudgetManager.Validation
{
    public abstract class ValidatableScreen<TViewModel> : Screen, IValidatable where TViewModel : PropertyChangedBase
    {
        private bool _canValidate;
        private AbstractValidator<TViewModel> _validator;

        public ValidatableScreen(AbstractValidator<TViewModel> validator)
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
                TViewModel instance = this as TViewModel;
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
            TViewModel instance = this as TViewModel;
            ValidationResult results = _validator.Validate(instance);
            return results.Errors;
        }

        public IEnumerable<ValidationFailure> ValidateProperty(string propertyName)
        {
            TViewModel instance = this as TViewModel;
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