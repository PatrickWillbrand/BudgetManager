using System;
using System.Data;
using System.Data.SQLite;
using BudgetManager.Data.Repositories;

namespace BudgetManager.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _dbConnection;
        private readonly IDbTransaction _dbTransaction;
        private IAccountRepository _accounts;
        private ICategoryRepository _categories;
        private bool _disposed;
        private bool _isCommited;
        private ITransactionRepository _transactions;

        public UnitOfWork(string connectionString)
        {
            _dbConnection = new SQLiteConnection(connectionString);
            _dbConnection.Open();
            _dbTransaction = _dbConnection.BeginTransaction();
        }

        public IAccountRepository Account
        {
            get
            {
                if (_accounts == null)
                {
                    _accounts = new AccountRepository(this);
                }

                return _accounts;
            }
        }

        public ICategoryRepository Categories
        {
            get
            {
                if (_categories == null)
                {
                    _categories = new CategoryRepository(this);
                }

                return _categories;
            }
        }

        public IDbConnection DbConnection
        {
            get { return _dbConnection; }
        }

        public IDbTransaction DbTransaction
        {
            get { return _dbTransaction; }
        }

        public ITransactionRepository Transactions
        {
            get
            {
                if (_transactions == null)
                {
                    _transactions = new TransactionRepository(this);
                }

                return _transactions;
            }
        }

        public void Commit()
        {
            if (_isCommited)
            {
                throw new InvalidOperationException("Unit of work already commited.");
            }

            try
            {
                _isCommited = true;
                _dbTransaction.Commit();
            }
            catch
            {
                Rollback();
                throw;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Rollback()
        {
            _dbTransaction.Rollback();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (!_isCommited)
                    {
                        Rollback();
                    }

                    _dbTransaction.Dispose();

                    if (_dbConnection.State == ConnectionState.Open)
                    {
                        _dbConnection.Close();
                        _dbConnection.Dispose();
                    }
                }
            }

            _disposed = true;
        }
    }
}