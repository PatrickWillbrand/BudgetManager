using BudgetManager.Domain;
using Caliburn.Micro;

namespace BudgetManager
{
    public class MainViewModel : Conductor<object>
    {
        private Account _account;

        public MainViewModel(Account account)
        {
            _account = account;
        }
    }
}