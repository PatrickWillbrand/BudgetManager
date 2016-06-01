using System;
using System.Collections.Generic;
using System.Linq;
using BudgetManager.Domain.Validators;
using FluentValidation.Results;

namespace BudgetManager.Domain
{
    public class Account : IValidatable
    {
        private AccountValidator _validator;

        public Account()
        {
            _validator = new AccountValidator();

            Transactions = new List<Transaction>();
        }

        public decimal Budget
        {
            get
            {
                decimal budget = 0;

                foreach (var item in Transactions)
                {
                    if (item.Direction == TransactionDirection.StartAmount || item.Direction == TransactionDirection.Income)
                    {
                        budget += item.Amount;
                    }
                    else
                    {
                        budget -= item.Amount;
                    }
                }

                return budget;
            }
        }

        public string FirstName { get; set; }

        public Guid Id { get; set; }

        public bool IsValid
        {
            get
            {
                ValidationResult results = _validator.Validate(this);
                return results.IsValid;
            }
        }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public IList<Transaction> Transactions { get; set; }

        public string UserName { get; set; }

        public IEnumerable<Transaction> GetTransactionsFromMonth(int year, int month)
        {
            return Transactions.Where(x => x.Date.Year == year && x.Date.Month == month);
        }

        public IEnumerable<Transaction> GetTransactionsFromYear(int year)
        {
            return Transactions.Where(x => x.Date.Year == year);
        }

        public IEnumerable<ValidationFailure> ValidateAll()
        {
            ValidationResult results = _validator.Validate(this);
            return results.Errors;
        }

        public IEnumerable<ValidationFailure> ValidateProperty(string propertyName)
        {
            ValidationResult results = _validator.Validate(this);
            return results.Errors.Where(x => x.PropertyName == propertyName);
        }
    }
}