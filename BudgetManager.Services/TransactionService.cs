using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetManager.Core.Configuration;
using BudgetManager.Data;
using BudgetManager.Data.Entities;
using BudgetManager.Domain;
using BudgetManager.Services.Mappings;

namespace BudgetManager.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IMapper<Transaction, TransactionEntity> _mapper;

        public TransactionService(IMapper<Transaction, TransactionEntity> mapper)
        {
            _mapper = mapper;
        }

        public async Task AddAsync(Transaction transaction)
        {
            transaction.Id = Guid.NewGuid();
            TransactionEntity entity = _mapper.Map(transaction);

            using (IUnitOfWork unitOfWork = new UnitOfWork(AppConfig.Config.ConnectionString))
            {
                await unitOfWork.Transactions.InsertAsync(entity);
                unitOfWork.Commit();
            }
        }

        public async Task<IEnumerable<Transaction>> GetAllByAccountAsync(Guid id)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork(AppConfig.Config.ConnectionString))
            {
                var entities = await unitOfWork.Transactions.GetAllByAccountAsync(id);
                var transactions = new List<Transaction>();

                foreach (var entity in entities)
                {
                    Transaction transaction = _mapper.Map(entity);
                    transactions.Add(transaction);
                }

                return transactions;
            }
        }
    }
}