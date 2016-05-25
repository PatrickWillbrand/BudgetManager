using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetManager.Data.Entities;

namespace BudgetManager.Data.Repositories
{
    public interface ICategoryRepository : IRepository<CategoryEntity>
    {
        Task<CategoryEntity> FindByIdAsync(Guid id);

        Task<IEnumerable<CategoryEntity>> GetAllAsync();

        Task InsertAsync(CategoryEntity entity);

        Task RemoveAsync(Guid id);

        Task RemoveAsync(CategoryEntity entity);

        Task UpdateAsync(CategoryEntity entity);
    }
}