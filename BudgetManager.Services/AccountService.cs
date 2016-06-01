using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetManager.Core.Configuration;
using BudgetManager.Core.Security;
using BudgetManager.Data;
using BudgetManager.Data.Entities;
using BudgetManager.Domain;
using BudgetManager.Services.Mappings;

namespace BudgetManager.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper<Account, AccountEntity> _accountMapper;
        private readonly IMapper<Transaction, TransactionEntity> _transactionMapper;

        public AccountService(IMapper<Account, AccountEntity> accountMapper,
                              IMapper<Transaction, TransactionEntity> transactionMapper)
        {
            _accountMapper = accountMapper;
            _transactionMapper = transactionMapper;
        }

        public async Task<Account> LoginAsync(string userName, string password)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork(AppConfig.Config.ConnectionString))
            {
                AccountEntity entity = await unitOfWork.Account.FindByUserNameAsync(userName);
                if (entity != null)
                {
                    Account account = _accountMapper.Map(entity);
                    if (ConfirmPassword(account, password))
                    {
                        account.Transactions = await GetAccountTransactionsAsync(unitOfWork, account.Id);
                        return account;
                    }
                }
            }

            throw new Exception("Benutzername und Passwort stimmen nicht überein.");
        }

        public async Task RegisterAsync(Account account, decimal startAmount)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork(AppConfig.Config.ConnectionString))
            {
                Guid id = Guid.NewGuid();
                account.Id = id;
                account.Salt = SaltCreator.CreateRandomSalt();
                account.Password += account.Salt;
                account.Password = HashFunctions.CreateSHA256(account.Password);

                AccountEntity accountEntity = _accountMapper.Map(account);
                var task1 = unitOfWork.Account.InsertAsync(accountEntity);

                Transaction transaction = CreateStartTransaction(startAmount, id);
                TransactionEntity transactionEntity = _transactionMapper.Map(transaction);
                var task2 = unitOfWork.Transactions.InsertAsync(transactionEntity);

                await Task.WhenAll(task1, task2);
                unitOfWork.Commit();
            }
        }

        private bool ConfirmPassword(Account account, string password)
        {
            string saltedAndHashedPassword = HashFunctions.CreateSHA256(password + account.Salt);
            return account.Password == saltedAndHashedPassword;
        }

        private Transaction CreateStartTransaction(decimal amount, Guid accountId)
        {
            return new Transaction
            {
                AccountId = accountId,
                Amount = amount,
                CategoryId = Guid.Empty,
                Date = DateTime.Now,
                Description = "Startbetrag",
                Direction = TransactionDirection.StartAmount,
                Id = Guid.NewGuid()
            };
        }

        private async Task<IList<Transaction>> GetAccountTransactionsAsync(IUnitOfWork unitOfWork, Guid accountId)
        {
            IEnumerable<TransactionEntity> entities = await unitOfWork.Transactions.GetAllByAccountAsync(accountId);
            return _transactionMapper.MapMany(entities).ToList();
        }
    }
}