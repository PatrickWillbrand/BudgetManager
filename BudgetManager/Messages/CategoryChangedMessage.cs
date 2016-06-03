using BudgetManager.Domain;

namespace BudgetManager.Messages
{
    public class CategoryChangedMessage
    {
        public Category NewCategory { get; set; }
    }
}