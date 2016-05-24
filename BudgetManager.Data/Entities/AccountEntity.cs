using System;

namespace BudgetManager.Data.Entities
{
    public class AccountEntity : IEntity
    {
        public string FirstName { get; set; }

        public Guid Id { get; set; }

        public string LastName { get; set; }
    }
}