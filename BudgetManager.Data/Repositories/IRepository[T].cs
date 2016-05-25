using BudgetManager.Data.Entities;

namespace BudgetManager.Data
{
    public interface IRepository<TEntity> : IRepository where TEntity : IEntity
    {
    }
}