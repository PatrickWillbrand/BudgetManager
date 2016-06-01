using BudgetManager.Domain;

namespace BudgetManager.Handles
{
    public class CategoryChangedMessage
    {
        public Category NewCategory { get; set; }
    }
}