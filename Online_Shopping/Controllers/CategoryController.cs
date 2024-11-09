using DTOs;
using DTOs.DTOs;
using DTOs.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts.Interfaces;

namespace Online_Shopping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService) 
        {
            _categoryService = categoryService;
        }

        [HttpPost("add-new-category")]
        public async Task<IActionResult> AddNewCategory([FromBody] RequestCategory request)
        {
            if (request.Name == null || request.Name == "")
            {
                return BadRequest("Category's name cannot null and must have at least 1 character");
            }
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

        [HttpGet("get-all-categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var allCategories = await _categoryService.GetAllCategory();
            if (allCategories == null)
                return NotFound();
            return Ok(allCategories);
        }

        [HttpGet("get-category/{Id}")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoryById(string Id)
        {
            var category = await _categoryService.GetCategoryById(Id);
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        [HttpDelete("delete-category/{Id}")]
        public async Task<ActionResult> DeleteCategoryById(string Id)
        {
            var category = await _categoryService.GetCategoryById(Id);
            if (category == null)
                return NotFound($"Cannot find category with Id: {Id}");

            await _categoryService.DeleteCategoryById(Id);
            return NoContent();
        }

        [HttpPatch("update-category/{Id}")]
        public async Task<ActionResult> UpdateCategory(string Id, [FromBody] RequestCategory request)
        {
            var cateId = await _categoryService.GetCategoryById(Id);
            if (cateId == null)
                return NotFound($"Cannot find CategoryId: {Id} to update");

            if (request.Name == null || request.Name == "")
                return BadRequest("Category's name cannot null and must have at least 1 character");
            try
            {
                await _categoryService.UpdateCategoryById(Id, request);
                return Ok(new Response<RequestCategory>
                {
                    Message = "Category is updated successfully",
                    Data = request
                });
            }
            catch
            {
                throw new Exception("Controller: Updating is failed");
            }

        }
    }
}