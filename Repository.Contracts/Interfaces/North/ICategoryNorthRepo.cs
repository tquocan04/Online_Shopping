using Entities.Entities.North;

namespace Repository.Contracts.Interfaces.North
{
    public interface ICategoryNorthRepo
    {
        Task CreateNewCategoryAsync(CategoryNorth category);
        Task DeleteCategoryByIdAsync(Guid categoryId);
        Task UpdateCategoryAsync(CategoryNorth category);
    }
}
