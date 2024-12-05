using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Shopping_North.Entities;
using Online_Shopping_North.Requests;
using Online_Shopping_North.Responses;
using Online_Shopping_North.Service.Contracts;

namespace Online_Shopping_North.Controllers
{
    [Route("api/categories/north")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("new-category")]
        public async Task<IActionResult> AddNewCategory([FromBody] Category category)
        {
            var newCategory = await _categoryService.CreateNewCategory(category);

            return CreatedAtAction(nameof(AddNewCategory), new { id = newCategory.Id });
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
