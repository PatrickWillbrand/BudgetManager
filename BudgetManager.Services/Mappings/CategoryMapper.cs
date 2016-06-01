using System.Collections.Generic;
using BudgetManager.Data.Entities;
using BudgetManager.Domain;

namespace BudgetManager.Services.Mappings
{
    public class CategoryMapper : IMapper<Category, CategoryEntity>
    {
        public CategoryEntity Map(Category item)
        {
            return new CategoryEntity
            {
                Description = item.Description,
                Id = item.Id,
                Name = item.Name
            };
        }

        public Category Map(CategoryEntity entity)
        {
            return new Category
            {
                Description = entity.Description,
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public IEnumerable<CategoryEntity> MapMany(IEnumerable<Category> items)
        {
            List<CategoryEntity> entities = new List<CategoryEntity>();

            foreach (var item in items)
            {
                CategoryEntity entity = Map(item);
                entities.Add(entity);
            }

            return entities;
        }

        public IEnumerable<Category> MapMany(IEnumerable<CategoryEntity> entities)
        {
            List<Category> items = new List<Category>();

            foreach (var entity in entities)
            {
                Category item = Map(entity);
                items.Add(item);
            }

            return items;
        }
    }
}