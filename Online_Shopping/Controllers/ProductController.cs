using AutoMapper;
using DTOs;
using DTOs.DTOs;
using DTOs.Request;
using Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts.Interfaces;
using Services.Services;

namespace Online_Shopping.Controllers
{
    [Route("api/[controller]")]
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

        [HttpPost("add-new-product")]
        public async Task<IActionResult> CreateNewProduct([FromBody] RequestProduct request)
        {
            if (request == null || request.Name == null || request.Name == "")
            {
                return BadRequest("Controller: Product cannot be null");
            }
            var newProduct = await _productService.CreateNewProduct(request);
            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.Id },
                new Response<RequestProduct>
                {
                    Message = "New product created successfully",
                    Data = request
                });
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProducts();
            if (products == null)
                return NotFound();
            return Ok(products);
        }

        [HttpGet("products/available")]
        public async Task<IActionResult> GetProductsNotHidden()
        {
            var products = await _productService.GetProductsNotHidden();
            if (products == null)
                return NotFound();
            return Ok(products);
        }

        [HttpGet("products/hidden")]
        public async Task<IActionResult> GetProductsHidden()
        {
            var products = await _productService.GetProductsHidden();
            if (products == null)
                return NotFound();
            return Ok(products);
        }

        [HttpGet("products/{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpPatch("products/{id}")]
        public async Task<IActionResult> UpdateStatusProductById(string id)
        {
            var prodduct = await _productService.GetProductById(id);
            if (prodduct == null)
                return NotFound();

            try
            {
                await _productService.UpdatestatusProduct(id);
                return Ok(new Response<string>
                {
                    Message = "The status is updated successfully"
                });
            }
            catch
            {
                return BadRequest(new Response<string>
                {
                    Message = "Product cannot be hidden"
                });
            }
        }

        [HttpPatch("products/update/{id}")]
        public async Task<IActionResult> UpdateInforProduct(string id,[FromBody] RequestProduct requestProduct)
        {
            var prodduct = await _productService.GetProductById(id);
            if (prodduct == null)
                return NotFound();
            try
            {
                await _productService.UpdateInforProduct(id, requestProduct);
                return Ok(new Response<string>
                {
                    Message = "The information is updated successfully"
                });
            }
            catch
            {
                return BadRequest(new Response<string>
                {
                    Message = "The information cannot be updated"
                });
            }
        }
    }
}
