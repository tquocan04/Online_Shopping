using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Online_Shopping.Context;
using Repository.Contracts.Interfaces;

namespace Repositories.Repositories
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly ApplicationContext _applicationContext;

        public CategoryRepo(ApplicationContext applicationContext) 
        {
            _applicationContext = applicationContext;
        }
        public async Task CreateNewCategoryAsync(string categoryName)
        {
            // check category
            if (await _applicationContext.Categories.AnyAsync(c => c.Name == categoryName))
            {
                throw new InvalidOperationException("Category name already exists.");
            }

            var newCategory = new Category
            { 
                Id = new Guid(),
                Name = categoryName 
            };

            await _applicationContext.Categories.AddAsync(newCategory);
            await _applicationContext.SaveChangesAsync();
        }
    }
}
