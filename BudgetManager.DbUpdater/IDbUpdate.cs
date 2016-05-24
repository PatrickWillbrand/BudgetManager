using System.Data;

namespace BudgetManager.DbUpdater
{
    public interface IDbUpdate
    {
        int Version { get; }

        void Update(IDbConnection connection, IDbTransaction transaction);
    }
}