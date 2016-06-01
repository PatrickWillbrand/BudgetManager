using System;
using BudgetManager.Dialogs;
using BudgetManager.Domain;
using BudgetManager.Services;
using BudgetManager.Validation;
using Caliburn.Micro;
using MaterialDesignThemes.Wpf;

namespace BudgetManager.Login
{
    public class LoginViewModel : ValidatableScreen<LoginViewModel>
    {
        private readonly IAccountService _accountService;
        private string _password;
        private string _userName;

        public LoginViewModel(IAccountService accountService) : base(new LoginViewModelValidator())
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

        public async void LoginAsync()
        {
            if (IsValid)
            {
                try
                {
                    Account account = await _accountService.LoginAsync(UserName, Password);
                    MainViewModel viewModel = new MainViewModel(account);
                    ((IConductor)Parent).ActivateItem(viewModel);
                }
                catch (Exception ex)
                {
                    MessageDialog dialog = new MessageDialog
                    {
                        Message = { Text = ex.Message },
                        Title = { Text = "Fehler bei der Anmeldung" }
                    };

                    await DialogHost.Show(dialog, "RootDialog");
                }
            }
        }

        public void ShowRegister()
        {
            var viewModel = IoC.Get<RegisterViewModel>();
            ((IConductor)Parent).ActivateItem(viewModel);
        }
    }
}