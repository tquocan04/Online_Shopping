using AutoMapper;
using DTOs;
using DTOs.DTOs;
using DTOs.Request;
using DTOs.Responses;
using Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts.Interfaces;
using Services.Services;

namespace Online_Shopping.Controllers
{
    [Route("api/products")]
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
        public async Task<IActionResult> CreateNewProduct([FromForm] RequestProduct request)
        {
            var newProduct = await _productService.CreateNewProduct(request);
            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.Id },
                new Response<Product>
                {
                    Message = "New product created successfully",
                    Data = newProduct
                });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProducts();
            if (products == null)
                return NotFound();
            return Ok(products);
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetProductsNotHidden()
        {
            var products = await _productService.GetProductsNotHidden();
            if (products == null)
                return NotFound();
            return Ok(products);
        }

        [HttpGet("hidden")]
        public async Task<IActionResult> GetProductsHidden()
        {
            var products = await _productService.GetProductsHidden();
            if (products == null)
                return NotFound();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
                return NotFound();
            return Ok(product);
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

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateInforProduct(string id, [FromForm] RequestProduct requestProduct)
        {
            var prodduct = await _productService.GetProductById(id);
            if (prodduct == null)
                return NotFound();
            await _productService.UpdateInforProduct(id, requestProduct);
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
