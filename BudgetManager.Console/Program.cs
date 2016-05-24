using BudgetManager.Core.Configuration;
using BudgetManager.DbUpdater;

namespace BudgetManager.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Updater updater = new Updater(AppConfig.Config.ConnectionString);
            updater.Update();
        }
    }
}