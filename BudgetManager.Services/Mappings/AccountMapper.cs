using System.Collections.Generic;
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

        public IEnumerable<AccountEntity> MapMany(IEnumerable<Account> items)
        {
            List<AccountEntity> entities = new List<AccountEntity>();

            foreach (var item in items)
            {
                AccountEntity entity = Map(item);
                entities.Add(entity);
            }

            return entities;
        }

        public IEnumerable<Account> MapMany(IEnumerable<AccountEntity> entities)
        {
            List<Account> items = new List<Account>();

            foreach (var entity in entities)
            {
                Account item = Map(entity);
                items.Add(item);
            }

            return items;
        }
    }
}