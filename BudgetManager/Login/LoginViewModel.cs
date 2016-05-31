using BudgetManager.Domain;
using BudgetManager.Services;
using Caliburn.Micro;

namespace BudgetManager.Login
{
    public class LoginViewModel : Screen
    {
        private readonly IAccountService _accountService;
        private Account _account;

        public LoginViewModel(IAccountService accountService)
        {
            _accountService = accountService;
            _account = new Account();
        }

        public string Password
        {
            get { return _account.Password; }
            set
            {
                _account.Password = value;
                NotifyOfPropertyChange(() => Password);
            }
        }

        public string UserName
        {
            get { return _account.UserName; }
            set
            {
                _account.UserName = value;
                NotifyOfPropertyChange(() => UserName);
            }
        }

        public void ShowRegister()
        {
            var viewModel = IoC.Get<RegisterViewModel>();
            ((IConductor)Parent).ActivateItem(viewModel);
        }
    }
}