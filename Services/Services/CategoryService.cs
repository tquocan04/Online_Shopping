using DTOs.DTOs;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;

namespace Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo _categoryRepo;

        public CategoryService(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }
        public async Task<CategoryDTO> CreateNewCategory(CategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
            {
                throw new ArgumentNullException("Category must have name");
            }
            await _categoryRepo.CreateNewCategoryAsync(categoryDTO.Name);
            return categoryDTO;
        }
    }
}
