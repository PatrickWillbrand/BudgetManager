using BudgetManager.Data.Entities;
using BudgetManager.Domain;

namespace BudgetManager.Services.Mappings
{
    public class AccountMapper : IMapper<Account, AccountEntity>
    {
        public AccountEntity Map(Account item)
        {
            return new AccountEntity
            {
                FirstName = item.FirstName,
                Id = item.Id,
                LastName = item.LastName,
                Password = item.Password,
                Salt = item.Salt,
                UserName = item.UserName
            };
        }

        public Account Map(AccountEntity entity)
        {
            return new Account
            {
                FirstName = entity.FirstName,
                Id = entity.Id,
                LastName = entity.LastName,
                Password = entity.Password,
                Salt = entity.Salt,
                UserName = entity.UserName
            };
        }
    }
}