namespace BudgetManager.Services.Mappings
{
    public interface IMapper<T, TEntity>
    {
        T Map(TEntity entity);

        TEntity Map(T item);
    }
}