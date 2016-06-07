using BudgetManager.Domain;

namespace BudgetManager.Messages
{
    public class CategorySelectedChangedMessage
    {
        public Category NewCategory { get; set; }
    }
}