using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetManager.Data.Entities;

namespace BudgetManager.Data.Repositories
{
    public interface IAccountRepository : IRepository<AccountEntity>
    {
        Task<AccountEntity> FindByIdAsync(Guid id);

        Task<AccountEntity> FindByUserNameAsync(string userName);

        Task<IEnumerable<AccountEntity>> GetAllAsync();

        Task InsertAsync(AccountEntity entity);

        Task RemoveAsync(Guid id);

        Task RemoveAsync(AccountEntity entity);

        Task UpdateAsync(AccountEntity entity);
    }
}