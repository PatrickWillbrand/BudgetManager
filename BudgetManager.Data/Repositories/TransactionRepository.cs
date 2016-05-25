using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetManager.Data.Entities;

namespace BudgetManager.Data.Repositories
{
    public class TransactionRepository : Repository<TransactionEntity>, ITransactionRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<TransactionEntity> FindByIdAsync(Guid id)
        {
            string sql = "SELECT * FROM Transactions WHERE Id = @Id";
            return ExecuteScalarAsync<TransactionEntity>(_unitOfWork.DbConnection, sql, new { Id = id });
        }

        public Task<IEnumerable<TransactionEntity>> GetAllByAccountAsync(Guid accountId)
        {
            string sql = "SELECT * FROM Transactions WHERE AccountId = @AccountId";
            return Query(_unitOfWork.DbConnection, sql, new { AccountId = accountId });
        }

        public Task InsertAsync(TransactionEntity entity)
        {
            string sql = @"INSERT INTO Transactions (Id, AccountId, Amount, CategoryId, Date, Description, Direction)
                           VALUES (@Id, @AccountId, @Amount, @CategoryId, @Date, @Description, @Direction)";
            return ExecuteAsync(_unitOfWork.DbConnection, sql, Mapping(entity), _unitOfWork.DbTransaction);
        }

        public Task RemoveAsync(TransactionEntity entity)
        {
            return RemoveAsync(entity.Id);
        }

        public Task RemoveAsync(Guid id)
        {
            string sql = "DELETE FROM Transactions WHERE Id = @Id";
            return ExecuteAsync(_unitOfWork.DbConnection, sql, new { Id = id }, _unitOfWork.DbTransaction);
        }

        public Task UpdateAsync(TransactionEntity entity)
        {
            string sql = @"UPDATE Transactions SET
                             AccountId = @AccountId,
                             Amount = @Amount,
                             CategoryId = @CategoryId,
                             Date = @Date,
                             Description = @Description,
                             Direction = @Direction
                           WHERE Id = @Id";
            return ExecuteAsync(_unitOfWork.DbConnection, sql, Mapping(entity), _unitOfWork.DbTransaction);
        }

        protected override dynamic Mapping(TransactionEntity item)
        {
            return new
            {
                Id = item.Id,
                AccountId = item.AccountId,
                Amount = item.Amount,
                CategoryId = item.CategoryId,
                Date = item.Date,
                Description = item.Description,
                Direction = item.Direction
            };
        }
    }
}