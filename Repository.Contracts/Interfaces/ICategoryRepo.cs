using Entities.Entities;

namespace Repository.Contracts.Interfaces
{
    public interface ICategoryRepo
    {
        Task CreateNewCategoryAsync(Category category);
        Task<IEnumerable<Category>> GetAllCategoryAsync();
        Task<Category> GetCategoryByIdAsync(string Id);
        Task DeleteCategoryByIdAsync(string categoryId);
        Task UpdateCategoryAsync(Category category);
    }
}
