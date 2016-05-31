using System;
using System.Threading.Tasks;
using BudgetManager.Core.Configuration;
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

        public Task LoginAsync(Account account)
        {
            throw new NotImplementedException();
        }

        public async Task RegisterAsync(Account account, decimal startAmount)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork(AppConfig.Config.ConnectionString))
            {
                Guid id = Guid.NewGuid();
                account.Id = id;

                AccountEntity accountEntity = _accountMapper.Map(account);
                var task1 = unitOfWork.Account.InsertAsync(accountEntity);

                Transaction transaction = CreateStartTransaction(startAmount, id);
                TransactionEntity transactionEntity = _transactionMapper.Map(transaction);
                var task2 = unitOfWork.Transactions.InsertAsync(transactionEntity);

                await Task.WhenAll(task1, task2);
                unitOfWork.Commit();
            }
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
    }
}