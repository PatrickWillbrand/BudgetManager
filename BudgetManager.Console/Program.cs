using System;
using System.Threading.Tasks;
using BudgetManager.Core.Configuration;
using BudgetManager.Data;
using BudgetManager.Data.Entities;
using BudgetManager.Domain;
using BudgetManager.Services;
using BudgetManager.Services.Mappings;

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
                    Guid account = Guid.NewGuid();
                    var mapper = new TransactionMapper();
                    var service = new TransactionService(uow, mapper);

                    var t1 = service.AddAsync(new Transaction
                    {
                        AccountId = account,
                        Amount = 20,
                        CategoryId = Guid.NewGuid(),
                        Date = DateTime.Now,
                        Description = "Gehalt",
                        Direction = TransactionDirection.Income
                    });

                    var t2 = service.AddAsync(new Transaction
                    {
                        AccountId = account,
                        Amount = 6.5m,
                        CategoryId = Guid.NewGuid(),
                        Date = DateTime.Now,
                        Description = "Miete",
                        Direction = TransactionDirection.Expense
                    });

                    await Task.WhenAll(t1, t2);

                    var result = await service.GetAllByAccountAsync(account);
                }
            }).Wait();
        }
    }
}