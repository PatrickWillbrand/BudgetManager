using BudgetManager.Domain;
using BudgetManager.Services;
using Caliburn.Micro;

namespace BudgetManager.Login
{
    public class RegisterViewModel : Screen
    {
        private readonly IAccountService _accountService;
        private Account _account;
        private decimal _startAmount;

        public RegisterViewModel(IAccountService accountService)
        {
            _accountService = accountService;
            _account = new Account();
        }

        public string FirstName
        {
            get { return _account.FirstName; }
            set
            {
                _account.FirstName = value;
                NotifyOfPropertyChange(() => FirstName);
            }
        }

        public string LastName
        {
            get { return _account.LastName; }
            set
            {
                _account.LastName = value;
                NotifyOfPropertyChange(() => LastName);
            }
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

        public decimal StartAmount
        {
            get { return _startAmount; }
            set
            {
                _startAmount = value;
                NotifyOfPropertyChange(() => StartAmount);
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

        public void NavigateBack()
        {
            var viewModel = IoC.Get<LoginViewModel>();
            ((IConductor)Parent).ActivateItem(viewModel);
        }
    }
}