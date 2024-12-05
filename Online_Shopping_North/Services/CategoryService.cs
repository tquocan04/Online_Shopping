using AutoMapper;
using Online_Shopping_North.DTOs;
using Online_Shopping_North.Entities;
using Online_Shopping_North.Repository.Contracts;
using Online_Shopping_North.Requests;
using Online_Shopping_North.Service.Contracts;

namespace Online_Shopping_North.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo _categoryRepo;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepo categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }


        public async Task<Category> CreateNewCategory(Category requestCategory)
        {
            await _categoryRepo.CreateNewCategoryAsync(requestCategory);

            return requestCategory;
        }

        public async Task DeleteCategoryById(string Id)
        {
            await _categoryRepo.DeleteCategoryByIdAsync(await _categoryRepo.GetCategoryByIdAsync(Guid.Parse(Id)));
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategory()
        {
            var allCategories = await _categoryRepo.GetAllCategoryAsync();

            return _mapper.Map<IEnumerable<CategoryDTO>>(allCategories);
        }

        public async Task<CategoryDTO> GetCategoryById(string Id)
        {
            var category = await _categoryRepo.GetCategoryByIdAsync(Guid.Parse(Id));
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task UpdateCategoryById(string Id, RequestCategory requestCategory)
        {
            var category = await _categoryRepo.GetCategoryByIdAsync(Guid.Parse(Id));
            
            _mapper.Map(requestCategory, category); // dto -> category
            await _categoryRepo.UpdateCategoryAsync(category);

        }
    }
}
