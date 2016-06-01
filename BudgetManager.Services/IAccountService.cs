using System.Threading.Tasks;
using BudgetManager.Domain;

namespace BudgetManager.Services
{
    public interface IAccountService
    {
        Task<Account> LoginAsync(string userName, string password);

        Task RegisterAsync(Account account, decimal startAmount);
    }
}