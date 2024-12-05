using DTOs;
using DTOs.DTOs;
using DTOs.Request;
using DTOs.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts.Interfaces;
using System.Net.Http;

namespace Online_Shopping.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly HttpClient _httpClient;
        private readonly string api = "http://localhost:5285/api/categories/north";

        public CategoryController(ICategoryService categoryService, HttpClient httpClient) 
        {
            _categoryService = categoryService;
            _httpClient = httpClient;
        }

        [HttpPost("new-category")]
        public async Task<IActionResult> AddNewCategory([FromBody] RequestCategory request)
        {
            var newCategory = await _categoryService.CreateNewCategory(request);

            var north = await _httpClient.PostAsJsonAsync($"{api}/new-category", newCategory);

            return CreatedAtAction(
                nameof(GetCategoryById),
                new { id = newCategory.Id },
                new Response<RequestCategory>
                {
                    Message = "New category created successfully!",
                    Data = request
                });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var allCategories = await _categoryService.GetAllCategory();
            if (allCategories == null)
            {
                return NotFound(new Response<string>
                {
                    Message = "Does not have any categories!"
                });
            }
            return Ok(allCategories);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCategoryById(string Id)
        {
            var category = await _categoryService.GetCategoryById(Id);
            if (category == null)
            {
                return NotFound(new Response<string>
                {
                    Message = "This category does not exist!"
                });
            }
            return Ok(category);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteCategoryById(string Id)
        {
            var category = await _categoryService.GetCategoryById(Id);
            if (category == null)
            {
                return NotFound($"Cannot find category with Id: {Id}");
            }

            await _categoryService.DeleteCategoryById(Id);

            await _httpClient.DeleteAsync($"{api}/{Id}");
            return NoContent();
        }

        [HttpPatch("{Id}")]
        public async Task<ActionResult> UpdateCategory(string Id, [FromBody] RequestCategory request)
        {
            var cateId = await _categoryService.GetCategoryById(Id);
            if (cateId == null)
            {
                return NotFound($"Cannot find CategoryId: {Id} to update");
            }

            bool check = await _categoryService.UpdateCategoryById(Id, request);
            if (!check)
            {
                return BadRequest(new Response<string>
                {
                    Message = "This name is existed!"
                });
            }

            await _httpClient.PatchAsJsonAsync($"{api}/{Id}", request);
            return Ok(new Response<RequestCategory>
            {
                Message = "Category is updated successfully",
                Data = request
            });
        }
    }
}