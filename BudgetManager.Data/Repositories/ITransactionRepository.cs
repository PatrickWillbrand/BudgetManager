using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetManager.Data.Entities;

namespace BudgetManager.Data.Repositories
{
    public interface ITransactionRepository : IRepository<TransactionEntity>
    {
        Task<TransactionEntity> FindByIdAsync(Guid id);

        Task<IEnumerable<TransactionEntity>> GetAllByAccountAsync(Guid accountId);

        Task InsertAsync(TransactionEntity entity);

        Task RemoveAsync(Guid id);

        Task RemoveAsync(TransactionEntity entity);

        Task UpdateAsync(TransactionEntity entity);
    }
}