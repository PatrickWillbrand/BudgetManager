using BudgetManager.Services;
using Caliburn.Micro;

namespace BudgetManager.Login
{
    public class LoginViewModel : Screen
    {
        private readonly IAccountService _accountService;
        private string _password;
        private string _userName;

        public LoginViewModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
            }
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
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