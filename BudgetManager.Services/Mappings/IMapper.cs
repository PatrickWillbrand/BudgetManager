using System.Collections.Generic;

namespace BudgetManager.Services.Mappings
{
    public interface IMapper<T, TEntity>
    {
        T Map(TEntity entity);

        TEntity Map(T item);

        IEnumerable<T> MapMany(IEnumerable<TEntity> entities);

        IEnumerable<TEntity> MapMany(IEnumerable<T> items);
    }
}