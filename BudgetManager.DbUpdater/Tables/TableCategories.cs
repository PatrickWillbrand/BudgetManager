namespace BudgetManager.DbUpdater.Tables
{
    public class TableCategories : NewTableUpdate
    {
        public const string Name = "Categories";

        public TableCategories(int version) : base(Name, version)
        {
            AddColumn("Id", "uniqueidentifier");
            AddColumn("Name", "varchar(100)");
            AddColumn("Description", "varchar(5000)", true);
            AddPrimaryKey("Id");
        }
    }
}