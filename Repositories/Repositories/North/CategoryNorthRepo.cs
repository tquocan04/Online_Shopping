using Entities.Context;
using Entities.Entities.North;
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

        public async Task CreateNewCategoryAsync(CategoryNorth category)
        {
            _applicationContextNorth.Categories.Add(category);
            await _applicationContextNorth.SaveChangesAsync();
        }
    }
}
