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

        private async Task<bool> checkNameExist(Category category)
        {
            if (await _applicationContext.Categories.AnyAsync(c => c.Name.ToLower() == category.Name.ToLower()))
                return true;
            return false;
        }

        public async Task CreateNewCategoryAsync(Category category)
        {
            // check category
            if (await checkNameExist(category))
            {
                throw new InvalidOperationException("Repo: Category name already exists.");
            }

            _applicationContext.Categories.Add(category);
            await _applicationContext.SaveChangesAsync();
        }
        public async Task<Category> GetCategoryByIdAsync(Guid Id)
        {
            return await _applicationContext.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == Id);
        }

        public async Task DeleteCategoryByIdAsync(Guid categoryId)
        {
            var cate = await _applicationContext.Categories.FindAsync(categoryId);
            
            if (cate != null)
            {
                _applicationContext.Categories.Remove(cate);
                await _applicationContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Category>> GetAllCategoryAsync()
        {
            return await _applicationContext.Categories.AsNoTracking().ToListAsync();
        }

        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            if (!await checkNameExist(category))
            {
                _applicationContext.Categories.Update(category);
                await _applicationContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        
    }
}