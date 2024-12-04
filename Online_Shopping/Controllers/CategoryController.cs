using DTOs;
using DTOs.DTOs;
using DTOs.Request;
using DTOs.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts.Interfaces;

namespace Online_Shopping.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService) 
        {
            _categoryService = categoryService;
        }

        [HttpPost("new-category")]
        public async Task<IActionResult> AddNewCategory([FromBody] RequestCategory request)
        {
            var newCategory = await _categoryService.CreateNewCategory(request);

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
                return NotFound();
            return Ok(allCategories);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCategoryById(string Id)
        {
            var category = await _categoryService.GetCategoryById(Id);
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteCategoryById(string Id)
        {
            var category = await _categoryService.GetCategoryById(Id);
            if (category == null)
                return NotFound($"Cannot find category with Id: {Id}");

            await _categoryService.DeleteCategoryById(Id);
            return NoContent();
        }

        [HttpPatch("{Id}")]
        public async Task<ActionResult> UpdateCategory(string Id, [FromBody] RequestCategory request)
        {
            var cateId = await _categoryService.GetCategoryById(Id);
            if (cateId == null)
                return NotFound($"Cannot find CategoryId: {Id} to update");

            await _categoryService.UpdateCategoryById(Id, request);
            return Ok(new Response<RequestCategory>
            {
                Message = "Category is updated successfully",
                Data = request
            });
        }
    }
}