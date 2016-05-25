using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetManager.Data.Entities;

namespace BudgetManager.Data.Repositories
{
    public class CategoryRepository : Repository<CategoryEntity>, ICategoryRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<CategoryEntity> FindByIdAsync(Guid id)
        {
            string sql = "SELECT * FROM Categories WHERE Id = @Id";
            return ExecuteScalarAsync<CategoryEntity>(_unitOfWork.DbConnection, sql, new { Id = id });
        }

        public Task<IEnumerable<CategoryEntity>> GetAllAsync()
        {
            string sql = "SELECT * FROM Categories";
            return Query(_unitOfWork.DbConnection, sql);
        }

        public Task InsertAsync(CategoryEntity entity)
        {
            string sql = @"INSERT INTO Categories (Id, Name, Description)
                           VALUES (@Id, @Name, @Description)";
            return ExecuteAsync(_unitOfWork.DbConnection, sql, Mapping(entity), _unitOfWork.DbTransaction);
        }

        public Task RemoveAsync(CategoryEntity entity)
        {
            return RemoveAsync(entity.Id);
        }

        public Task RemoveAsync(Guid id)
        {
            string sql = "DELETE FROM Categories WHERE Id = @Id";
            return ExecuteAsync(_unitOfWork.DbConnection, sql, new { Id = id }, _unitOfWork.DbTransaction);
        }

        public Task UpdateAsync(CategoryEntity entity)
        {
            string sql = @"UPDATE Categories SET
                             Name = @Name,
                             Description = @Description
                           WHERE Id = @Id";
            return ExecuteAsync(_unitOfWork.DbConnection, sql, Mapping(entity), _unitOfWork.DbTransaction);
        }

        protected override dynamic Mapping(CategoryEntity item)
        {
            return new
            {
                Id = item.Id,
                Description = item.Description,
                Name = item.Name
            };
        }
    }
}