using System;

namespace BudgetManager.Data.Entities
{
    public class TransactionEntity : IEntity
    {
        public Guid AccountId { get; set; }

        public decimal Amount { get; set; }

        public Guid CategoryId { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public TransactionDirection Direction { get; set; }

        public Guid Id { get; set; }
    }
}