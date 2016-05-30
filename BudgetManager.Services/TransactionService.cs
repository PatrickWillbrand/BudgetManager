using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetManager.Data;
using BudgetManager.Data.Entities;
using BudgetManager.Domain;
using BudgetManager.Services.Mappings;

namespace BudgetManager.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IMapper<Transaction, TransactionEntity> _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionService(IUnitOfWork unitOfWork, IMapper<Transaction, TransactionEntity> mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(Transaction transaction)
        {
            transaction.Id = Guid.NewGuid();

            TransactionEntity entity = _mapper.Map(transaction);
            await _unitOfWork.Transactions.InsertAsync(entity);
            _unitOfWork.Commit();
        }

        public async Task<IEnumerable<Transaction>> GetAllByAccountAsync(Guid id)
        {
            var entities = await _unitOfWork.Transactions.GetAllByAccountAsync(id);
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