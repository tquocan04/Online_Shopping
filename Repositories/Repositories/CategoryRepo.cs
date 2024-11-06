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
        public async Task<Category?> GetCategoryByIdAsync(Guid Id)
        {
            return await _applicationContext.Categories.FindAsync(Id);
        }

        public async Task DeleteCategoryByIdAsync(Guid categoryId)
        {
            var cate = await GetCategoryByIdAsync(categoryId);
            //var cate = await _applicationContext.Categories.FindAsync(categoryId);
            if (cate != null)
            {
                _applicationContext.Categories.Remove(cate);
                await _applicationContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Category>> GetAllCategoryAsync()
        {
            return await _applicationContext.Categories.ToListAsync();
        }

        public async Task<Category?> UpdateCategoryAsync(Category category)
        {
            var cate = GetCategoryByIdAsync(category.Id);
            if (cate != null)
            {
                _applicationContext.Categories.Update(category);
                await _applicationContext.SaveChangesAsync();
                return category;
            }
            return null;
        }
    }
        
}
