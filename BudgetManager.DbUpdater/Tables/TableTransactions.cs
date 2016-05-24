namespace BudgetManager.DbUpdater.Tables
{
    public class TableTransactions : NewTableUpdate
    {
        public const string Name = "Transactions";

        public TableTransactions(int version) : base(Name, version)
        {
            AddColumn("Id", "uniqueidentifier");
            AddColumn("AccountId", "uniqueidentifier");
            AddColumn("Amount", "decimal");
            AddColumn("CategoryId", "uniqueidentifier");
            AddColumn("Date", "datetime");
            AddColumn("Description", "varchar(1000)");
            AddColumn("Direction", "integer");
            AddPrimaryKey("Id");
        }
    }
}