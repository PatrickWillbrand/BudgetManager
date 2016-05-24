using System;

namespace BudgetManager.Data.Entities
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}