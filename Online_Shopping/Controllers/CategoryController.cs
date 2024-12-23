using DTOs.Request;
using DTOs.Responses;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin, Staff")]
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
        [Authorize(Roles = "Admin, Staff")]
        public async Task<ActionResult> DeleteCategoryById(string Id)
        {
            var category = await _categoryService.GetCategoryById(Id);
            if (category == null)
            {
                return NotFound($"Cannot find category with Id: {Id}");
            }

            await _categoryService.DeleteCategoryById(Id);

            return NoContent();
        }

        [HttpPatch("{Id}")]
        [Authorize(Roles = "Admin, Staff")]
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

            return Ok(new Response<RequestCategory>
            {
                Message = "Category is updated successfully",
                Data = request
            });
        }
    }
}