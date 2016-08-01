using BudgetManager.Categories;
using BudgetManager.Dashboard;
using BudgetManager.Domain;
using BudgetManager.Settings;
using BudgetManager.Transactions;
using Caliburn.Micro;

namespace BudgetManager
{
    public class MainViewModel : Conductor<object>.Collection.AllActive
    {
        private Account _account;

        public MainViewModel(Account account)
        {
            _account = account;


            Items.Add(new DashboardWorkspaceViewModel() { DisplayName = "Dashboard" });
            Items.Add(new TransactionsWorkspaceViewModel() { DisplayName = "Buchungen" });
            Items.Add(new CategoryWorkspaceViewModel() { DisplayName = "Kategorien" });
            Items.Add(new SettingsWorkspaceViewModel() { DisplayName = "Einstellungen" });
        }
    }
}