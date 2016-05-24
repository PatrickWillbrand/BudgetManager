using System;

namespace BudgetManager.Data.Entities
{
    public class CategoryEntity : IEntity
    {
        public string Description { get; set; }

        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}