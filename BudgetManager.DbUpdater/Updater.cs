using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using BudgetManager.DbUpdater.Tables;
using Dapper;

namespace BudgetManager.DbUpdater
{
    public class Updater
    {
        private readonly string _connectionString;
        private readonly int _currentVersion;
        private IReadOnlyCollection<IDbUpdate> _updates;

        public Updater(string connectionString)
        {
            _connectionString = connectionString;
            _currentVersion = GetCurrentVersion();
            _updates = new ReadOnlyCollection<IDbUpdate>(GetUpdates());
        }

        public void Update()
        {
            using (IDbConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    foreach (var item in _updates)
                    {
                        if (_currentVersion < item.Version)
                        {
                            item.Update(connection, transaction);
                            SaveNewVersion(connection, transaction, item.Version);
                        }
                    }

                    transaction.Commit();
                }
            }
        }

        private int GetCurrentVersion()
        {
            try
            {
                using (IDbConnection connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    IEnumerable<int> result = connection.Query(
                        @"SELECT Version
                          FROM Versions
                          ORDER BY Version DESC
                          LIMIT 1").Select(row => (int)row.Version);

                    if (result.Any())
                    {
                        return result.FirstOrDefault();
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch
            {
                return 0;
            }
        }

        private IList<IDbUpdate> GetUpdates()
        {
            var updates = new UpdateList();
            updates.Add(new TableVersions(1));
            updates.Add(new TableAccounts(GetVersion(updates)));
            updates.Add(new TableCategories(GetVersion(updates)));
            updates.Add(new TableTransactions(GetVersion(updates)));
            return updates.Updates.ToList();
        }

        private int GetVersion(UpdateList updates)
        {
            int version = updates.Updates.Max(x => x.Version);
            return version + 1;
        }

        private void SaveNewVersion(IDbConnection connection, IDbTransaction transaction, int version)
        {
            connection.Execute(@"INSERT INTO Versions (Version, Date) VALUES (@Version, @Date)", new
            {
                @Version = version,
                @Date = DateTime.Now
            }, transaction);
        }
    }
}