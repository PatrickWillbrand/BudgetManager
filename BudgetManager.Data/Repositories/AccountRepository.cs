using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetManager.Data.Entities;

namespace BudgetManager.Data.Repositories
{
    public class AccountRepository : Repository<AccountEntity>, IAccountRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<AccountEntity> FindByIdAsync(Guid id)
        {
            string sql = "SELECT * FROM Accounts WHERE Id = @Id";
            return ExecuteScalarAsync<AccountEntity>(_unitOfWork.DbConnection, sql, new { Id = id });
        }

        public Task<IEnumerable<AccountEntity>> GetAllAsync()
        {
            string sql = "SELECT * FROM Accounts";
            return Query(_unitOfWork.DbConnection, sql);
        }

        public Task InsertAsync(AccountEntity entity)
        {
            string sql = @"INSERT INTO Accounts (Id, FirstName, LastName)
                           VALUES (@Id, @FirstName, @LastName)";
            return ExecuteAsync(_unitOfWork.DbConnection, sql, Mapping(entity), _unitOfWork.DbTransaction);
        }

        public Task RemoveAsync(AccountEntity entity)
        {
            return RemoveAsync(entity.Id);
        }

        public Task RemoveAsync(Guid id)
        {
            string sql = "DELETE FROM Accounts WHERE Id = @Id";
            return ExecuteAsync(_unitOfWork.DbConnection, sql, new { Id = id });
        }

        public Task UpdateAsync(AccountEntity entity)
        {
            string sql = @"UPDATE Accounts SET
                             FirstName = @FirstName,
                             LastName = @LastName
                           WHERE Id = @Id";
            return ExecuteAsync(_unitOfWork.DbConnection, sql, Mapping(entity), _unitOfWork.DbTransaction);
        }

        protected override dynamic Mapping(AccountEntity item)
        {
            return new
            {
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName
            };
        }
    }
}