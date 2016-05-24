namespace BudgetManager.DbUpdater.Tables
{
    public class TableVersions : NewTableUpdate
    {
        public const string Name = "Versions";

        public TableVersions(int version) : base(Name, version)
        {
            AddColumn("Version", "integer");
            AddColumn("Date", "datetime");
            AddPrimaryKey("Version");
        }
    }
}