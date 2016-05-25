using System;
using System.Data;
using BudgetManager.Data.Repositories;

namespace BudgetManager.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository Account { get; }

        ICategoryRepository Categories { get; }

        IDbConnection DbConnection { get; }

        IDbTransaction DbTransaction { get; }

        ITransactionRepository Transactions { get; }

        void Commit();

        void Rollback();
    }
}