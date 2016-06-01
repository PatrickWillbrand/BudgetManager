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

        public Task AddAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork(AppConfig.Config.ConnectionString))
            {
                IEnumerable<CategoryEntity> entites = await unitOfWork.Categories.GetAllAsync();
                return _mapper.MapMany(entites);
            }
        }
    }
}