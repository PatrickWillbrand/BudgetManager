using System;
using System.Threading.Tasks;
using BudgetManager.Domain;

namespace BudgetManager.Services
{
    public class AccountService : IAccountService
    {
        public Task LoginAsync(Account account)
        {
            throw new NotImplementedException();
        }

        public Task RegisterAsync(Account account, decimal startAmount)
        {
            throw new NotImplementedException();
        }
    }
}