using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetManager.Domain;

namespace BudgetManager.Services
{
    public interface ITransactionService
    {
        Task AddAsync(Transaction transaction);

        Task<IEnumerable<Transaction>> GetAllByAccountAsync(Guid id);
    }
}