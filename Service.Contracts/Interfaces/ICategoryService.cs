using DTOs.DTOs;

namespace Service.Contracts.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDTO> CreateNewCategory(CategoryDTO categoryDTO); 
    }
}
