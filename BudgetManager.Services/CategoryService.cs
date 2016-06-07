using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetManager.Core.Configuration;
using BudgetManager.Data;
using BudgetManager.Data.Entities;
using BudgetManager.Domain;
using BudgetManager.Services.Mappings;

namespace BudgetManager.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper<Category, CategoryEntity> _mapper;

        public CategoryService(IMapper<Category, CategoryEntity> mapper)
        {
            _mapper = mapper;
        }

        public async Task AddAsync(Category category)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork(AppConfig.Config.ConnectionString))
            {
                category.Id = Guid.NewGuid();

                CategoryEntity entity = _mapper.Map(category);
                await unitOfWork.Categories.InsertAsync(entity);
                unitOfWork.Commit();
            }
        }

        public async Task EditAsync(Category category)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork(AppConfig.Config.ConnectionString))
            {
                CategoryEntity entity = _mapper.Map(category);
                await unitOfWork.Categories.UpdateAsync(entity);
                unitOfWork.Commit();
            }
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork(AppConfig.Config.ConnectionString))
            {
                IEnumerable<CategoryEntity> entites = await unitOfWork.Categories.GetAllAsync();
                return _mapper.MapMany(entites);
            }
        }

        public async Task RemoveAsync(Category category)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork(AppConfig.Config.ConnectionString))
            {
                CategoryEntity entity = _mapper.Map(category);
                await unitOfWork.Categories.RemoveAsync(entity);
                unitOfWork.Commit();
            }
        }
    }
}