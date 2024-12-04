using Entities.Context;
using Entities.Entities;
using Entities.Entities.North;
using Microsoft.EntityFrameworkCore;
using Online_Shopping.Context;

namespace Repository.Contracts.Interfaces.North
{
    public class CategoryNorthRepo : ICategoryNorthRepo
    {
        private readonly ApplicationContextNorth _applicationContextNorth;

        public CategoryNorthRepo(ApplicationContextNorth applicationContextNorth) 
        {
            _applicationContextNorth = applicationContextNorth;
        }

        private async Task<bool> checkNameExist(CategoryNorth category)
        {
            if (await _applicationContextNorth.Categories.AnyAsync(c => c.Name == category.Name))
                return true;
            return false;
        }

        public async Task CreateNewCategoryAsync(CategoryNorth category)
        {
            if (await checkNameExist(category))
            {
                throw new InvalidOperationException("RepoNorth: Category name already exists.");
            }
            _applicationContextNorth.Categories.Add(category);
            await _applicationContextNorth.SaveChangesAsync();
        }

        public async Task DeleteCategoryByIdAsync(Guid categoryId)
        {
            var cate = await _applicationContextNorth.Categories.FindAsync(categoryId);

            if (cate != null)
            {
                _applicationContextNorth.Categories.Remove(cate);
                await _applicationContextNorth.SaveChangesAsync();
            }
        }

        public async Task UpdateCategoryAsync(CategoryNorth category)
        {
            var cate = await _applicationContextNorth.Categories.FindAsync(category.Id); //await => doi tuong dang bi DbContext Theo doi -> phai dung CurrentValues.SetValues()
            if (cate != null && cate.Name != category.Name)
            {
                if (!await checkNameExist(category))
                {
                    cate.Name = category.Name;
                    await _applicationContextNorth.SaveChangesAsync();

                }
            }
        }
    }
}
