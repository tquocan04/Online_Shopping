using AutoMapper;
using DTOs.DTOs;
using Entities.Entities;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;
using System.Collections.Generic;

namespace Services.Services
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

        public async Task<Category> CreateNewCategory(CategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
            {
                throw new ArgumentNullException("Category must have name");
            }
            await _categoryRepo.CreateNewCategoryAsync(categoryDTO.Name);
            return _mapper.Map<Category>(categoryDTO);
        }

        public async Task DeleteCategoryById(Guid Id)
        {
            await _categoryRepo.DeleteCategoryByIdAsync(Id);
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategory()
        {
            var allCategories = await _categoryRepo.GetAllCategoryAsync();
            
            return _mapper.Map<IEnumerable<CategoryDTO>>(allCategories);
        }

        public async Task<CategoryDTO> GetCategoryById(Guid Id)
        {
            var category = await _categoryRepo.GetCategoryByIdAsync(Id);
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<CategoryDTO> UpdateCategoryById(Guid Id, CategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
            {
                throw new ArgumentNullException($"{nameof(categoryDTO)}");
            }
            var category = await _categoryRepo.GetCategoryByIdAsync(Id);
            if (category == null)
            {
                throw new ArgumentException($"Cannot find CatergoryId: {Id}");
            }
            _mapper.Map(categoryDTO, category); // dto -> category
            await _categoryRepo.UpdateCategoryAsync(category);
            return categoryDTO;
        }
    }
}
