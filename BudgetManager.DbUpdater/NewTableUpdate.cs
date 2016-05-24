using System;
using System.Collections.Generic;
using System.Data;

namespace BudgetManager.DbUpdater
{
    public class NewTableUpdate : IDbUpdate
    {
        private List<Column> _columns = new List<Column>();
        private string _primaryKey;

        public NewTableUpdate(string tableName, int version)
        {
            TableName = tableName;
            Version = version;
        }

        public string TableName { get; private set; }

        public int Version { get; private set; }

        public void AddColumn(string name, string type, bool canNull = false, string defaultValue = "", string additional = "")
        {
            var column = new Column(name);
            column.Additional = additional;
            column.CanNull = canNull;
            column.Default = defaultValue;
            column.Type = type;
            _columns.Add(column);
        }

        public void AddPrimaryKey(params string[] primaryKey)
        {
            if (primaryKey == null || primaryKey.Length == 0)
            {
                throw new ArgumentNullException(nameof(primaryKey));
            }

            string keys = string.Join(",", primaryKey);
            _primaryKey = $"PRIMARY KEY ({keys})";
        }

        public void Update(IDbConnection connection, IDbTransaction transaction)
        {
            using (IDbCommand command = connection.CreateCommand())
            {
                command.Transaction = transaction;
                command.CommandText = CreateCommandText();
                command.ExecuteNonQuery();
            }
        }

        private string CreateCommandText()
        {
            if (string.IsNullOrWhiteSpace(_primaryKey))
            {
                throw new ArgumentNullException(nameof(_primaryKey));
            }

            string commandText = string.Empty;
            commandText += $" CREATE TABLE {TableName} (";

            foreach (var col in _columns)
            {
                commandText += $"{col.ColumnName} {col.Type}";

                if (col.CanNull)
                {
                    commandText += " NULL ";
                }
                else
                {
                    commandText += " NOT NULL ";
                }

                if (!string.IsNullOrWhiteSpace(col.Default))
                {
                    commandText += $" DEFAULT {col.Default}";
                }

                if (!string.IsNullOrWhiteSpace(col.Additional))
                {
                    commandText += $" {col.Additional} ";
                }

                commandText += ",";
            }

            commandText += _primaryKey;
            commandText += ")";
            return commandText;
        }
    }
}