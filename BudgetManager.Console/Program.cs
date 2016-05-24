using BudgetManager.DbUpdater;

namespace BudgetManager.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string connectionString = @"Data Source=C:\TEST\budget.sqlite";

            Updater updater = new Updater(connectionString);
            updater.Update();
        }
    }
}