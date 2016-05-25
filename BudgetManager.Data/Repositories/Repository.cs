using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BudgetManager.Data.Entities;
using Dapper;

namespace BudgetManager.Data
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {
        protected async Task<int> ExecuteAsync(IDbConnection connection, string sql, object param = null, IDbTransaction transaction = null)
        {
            IEnumerable<int> result = await connection.QueryAsync<int>(sql, param, transaction);
            int affectedRows = result.FirstOrDefault();
            return affectedRows;
        }

        protected async Task<T> ExecuteScalarAsync<T>(IDbConnection connection, string sql, object param = null, IDbTransaction transaction = null)
        {
            IEnumerable<T> result = await connection.QueryAsync<T>(sql, param, transaction);
            T entity = result.FirstOrDefault();
            return entity;
        }

        protected abstract dynamic Mapping(TEntity item);

        protected async Task<IEnumerable<TEntity>> Query(IDbConnection connection, string sql, object param = null, IDbTransaction transaction = null)
        {
            IEnumerable<TEntity> result = await connection.QueryAsync<TEntity>(sql, param, transaction);
            return result;
        }
    }
}