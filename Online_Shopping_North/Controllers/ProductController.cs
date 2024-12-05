using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Shopping_North.Entities;
using Online_Shopping_North.Requests;
using Online_Shopping_North.Responses;
using Online_Shopping_North.Service.Contracts;

namespace Online_Shopping_North.Controllers
{
    [Route("api/products/north")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpPost("new-product")]
        public async Task<IActionResult> CreateNewProduct([FromBody] Product product)
        {
            var newProduct = await _productService.CreateNewProduct(product);
            return CreatedAtAction(nameof(CreateNewProduct), new { id = newProduct.Id });
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateStatusProductById(string id)
        {
            var prodduct = await _productService.GetProductById(id);
            if (prodduct == null)
                return NotFound();

            await _productService.UpdatestatusProduct(id);
            return Ok(new Response<string>
            {
                Message = "The status is updated successfully"
            });

        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateInforProduct([FromBody] Product requestProduct)
        {
            var prodduct = await _productService.GetProductById(requestProduct.Id.ToString());
            if (prodduct == null)
                return NotFound();
            await _productService.UpdateInforProduct(requestProduct);
            return Ok(new Response<string>
            {
                Message = "The information is updated successfully"
            });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productService.DeleteProduct(id);
            return NoContent();
        }
    }
}
