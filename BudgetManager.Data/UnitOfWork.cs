using System;
using System.Data;
using System.Data.SQLite;
using BudgetManager.Data.Repositories;

namespace BudgetManager.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private IAccountRepository _accounts;
        private ICategoryRepository _categories;
        private IDbConnection _dbConnection;
        private IDbTransaction _dbTransaction;
        private bool _disposed;
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
            try
            {
                _dbTransaction.Commit();
            }
            catch
            {
                Rollback();
                throw;
            }
            finally
            {
                _dbTransaction.Dispose();
                _dbTransaction = _dbConnection.BeginTransaction();

                Clear();
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
                    if (_dbTransaction != null)
                    {
                        Rollback();
                        _dbTransaction.Dispose();
                    }

                    if (_dbConnection.State == ConnectionState.Open)
                    {
                        _dbConnection.Close();
                        _dbConnection.Dispose();
                    }
                }
            }

            _disposed = true;
        }

        private void Clear()
        {
            _accounts = null;
            _categories = null;
            _transactions = null;
        }
    }
}