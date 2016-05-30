using BudgetManager.Login;
using Caliburn.Micro;

namespace BudgetManager
{
    public class ShellViewModel : Conductor<object>
    {
        public ShellViewModel()
        {
            DisplayName = "Budget Manager";
        }

        protected override void OnActivate()
        {
            var viewModel = IoC.Get<LoginViewModel>();
            ActivateItem(viewModel);
        }
    }
}