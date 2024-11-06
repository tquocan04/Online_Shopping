using DTOs.DTOs;
using Entities.Entities;
using System.Collections.Generic;

namespace Service.Contracts.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> CreateNewCategory(CategoryDTO categoryDTO);
        Task<IEnumerable<CategoryDTO>> GetAllCategory();
        Task<CategoryDTO> GetCategoryById(Guid Id);
        Task DeleteCategoryById(Guid Id);
        Task<CategoryDTO> UpdateCategoryById(Guid Id, CategoryDTO categoryDTO);

    }
}
