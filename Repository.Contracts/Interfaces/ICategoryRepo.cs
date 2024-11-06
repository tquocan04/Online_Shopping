using Entities.Entities;

namespace Repository.Contracts.Interfaces
{
    public interface ICategoryRepo
    {
        Task CreateNewCategoryAsync(string categoryName);
        Task<IEnumerable<Category>> GetAllCategoryAsync();
        Task<Category?> GetCategoryByIdAsync(Guid Id);
        Task DeleteCategoryByIdAsync(Guid categoryId);
        Task<Category?> UpdateCategoryAsync(Category category);
    }
}
