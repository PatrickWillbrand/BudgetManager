using BudgetManager.Services;
using Caliburn.Micro;

namespace BudgetManager.Login
{
    public class LoginViewModel : Screen
    {
        private readonly IAccountService _accountService;

        public LoginViewModel(IAccountService accountService)
        {
            _accountService = accountService;
        }
    }
}