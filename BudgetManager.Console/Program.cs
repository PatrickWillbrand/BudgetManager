using System;
using System.Threading.Tasks;
using BudgetManager.Core.Configuration;
using BudgetManager.Data;
using BudgetManager.Data.Entities;

namespace BudgetManager.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                using (IUnitOfWork uow = new UnitOfWork(AppConfig.Config.ConnectionString))
                {
                    var entity = new CategoryEntity
                    {
                        Id = Guid.NewGuid(),
                        Name = "Miete",
                        Description = string.Empty
                    };

                    await uow.Categories.InsertAsync(entity);
                    
                    uow.Commit();

                    var category = uow.Categories.FindByIdAsync(entity.Id);
                }
            }).Wait();
        }
    }
}