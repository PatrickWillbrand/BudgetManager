using System;
using System.Collections.Generic;

namespace BudgetManager.DbUpdater
{
    public class UpdateList
    {
        private IList<IDbUpdate> _updates;

        public UpdateList()
        {
            _updates = new List<IDbUpdate>();
        }

        public IEnumerable<IDbUpdate> Updates
        {
            get
            {
                return _updates;
            }
        }

        public void Add(IDbUpdate update)
        {
            if (_updates.Contains(update))
            {
                throw new ArgumentException("The given update already exists.");
            }

            _updates.Add(update);
        }
    }
}