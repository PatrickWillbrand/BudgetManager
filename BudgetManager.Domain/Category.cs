using System;

namespace BudgetManager.Domain
{
    public class Category
    {
        public string Description { get; set; }

        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}