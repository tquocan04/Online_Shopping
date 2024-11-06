using DTOs.DTOs;
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
        public async Task<IActionResult> AddNewCategory([FromBody] CategoryDTO categoryDTO)
        {
            if (categoryDTO.Name == null || categoryDTO.Name == "")
            {
                return BadRequest("Category's name cannot null and must have at least 1 character");
            }
            var newCategory = await _categoryService.CreateNewCategory(categoryDTO);
            categoryDTO.Name = newCategory.Name;
            return CreatedAtAction("GetCategoryById", new { id = newCategory.Id }, categoryDTO);
        }

        [HttpGet("get-all-categories")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAllCategories()
        {
            var allCategories = await _categoryService.GetAllCategory();
            if (allCategories == null)
                return NotFound();
            return Ok(allCategories);
        }

        [HttpGet("get-all-categories/{Id}")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoryById(Guid Id)
        {
            var category = await _categoryService.GetCategoryById(Id);
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        [HttpDelete("delete-category/{Id}")]
        public async Task<ActionResult> DeleteCategoryById(Guid Id)
        {
            var category = await _categoryService.GetCategoryById(Id);
            if (category == null)
                return NotFound($"Cannot find category with Id: {Id}");

            await _categoryService.DeleteCategoryById(Id);
            return NoContent();
        }

        [HttpPatch("update-category/{Id}")]
        public async Task<ActionResult> UpdateCategory(Guid Id, [FromBody] CategoryDTO categoryDTO)
        {
            var cateId = await _categoryService.GetCategoryById(Id);
            if (cateId == null)
                return NotFound($"Cannot find CategoryId: {Id} to update");

            if (categoryDTO.Name == null || categoryDTO.Name == "")
                return BadRequest("Category's name cannot null and must have at least 1 character");

            var category = await _categoryService.UpdateCategoryById(Id, categoryDTO);
            return Ok(category);
        }
    }
}
