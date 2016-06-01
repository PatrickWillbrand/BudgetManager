namespace BudgetManager.DbUpdater.Tables
{
    public class TableAccounts : NewTableUpdate
    {
        public const string Name = "Accounts";

        public TableAccounts(int version) : base(Name, version)
        {
            AddColumn("Id", "uniqueidentifier");
            AddColumn("FirstName", "varchar(100)");
            AddColumn("LastName", "varchar(100)");
            AddColumn("UserName", "varchar(100)");
            AddColumn("Password", "varchar(1000)");
            AddColumn("Salt", "varchar(1000)");
            AddPrimaryKey("Id");
        }
    }
}