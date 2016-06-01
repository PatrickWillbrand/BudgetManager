using System;
using System.Collections.Generic;
using System.Linq;

namespace BudgetManager.Domain
{
    public class Account
    {
        public Account()
        {
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
    }
}