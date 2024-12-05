﻿using Online_Shopping_North.DTOs;
using Online_Shopping_North.Entities;
using Online_Shopping_North.Requests;

namespace Online_Shopping_North.Service.Contracts
{
    public interface ICategoryService
    {
        Task<Category> CreateNewCategory(Category requestCategory);
        Task<IEnumerable<CategoryDTO>> GetAllCategory();
        Task<CategoryDTO> GetCategoryById(string Id);
        Task DeleteCategoryById(string Id);
        Task UpdateCategoryById(string Id, RequestCategory requestCategory);
    }
}
