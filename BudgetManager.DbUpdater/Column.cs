namespace BudgetManager.DbUpdater
{
    public class Column
    {
        public Column(string columnName)
        {
            ColumnName = columnName;
        }

        public string Additional { get; set; }

        public bool CanNull { get; set; }

        public string ColumnName { get; private set; }

        public string Default { get; set; }

        public string Type { get; set; }
    }
}