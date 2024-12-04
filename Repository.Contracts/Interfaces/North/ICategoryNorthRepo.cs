using Entities.Entities.North;

namespace Repository.Contracts.Interfaces.North
{
    public interface ICategoryNorthRepo
    {
        Task CreateNewCategoryAsync(CategoryNorth category);
    }
}
