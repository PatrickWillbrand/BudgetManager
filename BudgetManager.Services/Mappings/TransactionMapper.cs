using System;
using System.Collections.Generic;
using BudgetManager.Data.Entities;
using BudgetManager.Domain;

namespace BudgetManager.Services.Mappings
{
    public class TransactionMapper : IMapper<Transaction, TransactionEntity>
    {
        public TransactionEntity Map(Transaction item)
        {
            return new TransactionEntity
            {
                AccountId = item.AccountId,
                Amount = item.Amount,
                CategoryId = item.CategoryId,
                Date = item.Date,
                Description = item.Description,
                Direction = (int)item.Direction,
                Id = item.Id
            };
        }

        public Transaction Map(TransactionEntity entity)
        {
            return new Transaction
            {
                AccountId = entity.AccountId,
                Amount = entity.Amount,
                CategoryId = entity.CategoryId,
                Date = entity.Date,
                Description = entity.Description,
                Direction = (TransactionDirection)Enum.ToObject(typeof(TransactionDirection), entity.Direction),
                Id = entity.Id
            };
        }

        public IEnumerable<TransactionEntity> MapMany(IEnumerable<Transaction> items)
        {
            List<TransactionEntity> entities = new List<TransactionEntity>();

            foreach (var item in items)
            {
                TransactionEntity entity = Map(item);
                entities.Add(entity);
            }

            return entities;
        }

        public IEnumerable<Transaction> MapMany(IEnumerable<TransactionEntity> entities)
        {
            List<Transaction> items = new List<Transaction>();

            foreach (var entity in entities)
            {
                Transaction item = Map(entity);
                items.Add(item);
            }

            return items;
        }
    }
}