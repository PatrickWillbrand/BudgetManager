using BudgetManager.Domain;

namespace BudgetManager.Messages
{
    public class CategorySavedMessage
    {
        public Category Category { get; set; }

        public bool IsRemoved { get; set; }
    }
}