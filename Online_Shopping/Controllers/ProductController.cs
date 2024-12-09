using AutoMapper;
using DTOs;
using DTOs.DTOs;
using DTOs.Request;
using DTOs.Responses;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts.Interfaces;
using Services.Services;
using System.Net.Http;

namespace Online_Shopping.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        //private readonly string api = "http://localhost:5285/api/products/north";

        public ProductController(IProductService productService, IMapper mapper,
            HttpClient httpClient) 
        {
            _productService = productService;
            _mapper = mapper;
            _httpClient = httpClient;
        }

        [HttpPost("new-product")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> CreateNewProduct([FromForm] RequestProduct request)
        {
            var newProduct = await _productService.CreateNewProduct(request);

            //var north = await _httpClient.PostAsJsonAsync($"{api}/new-product", newProduct);
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
            if (!products.Any())
            {
                return NotFound(new Response<IEnumerable<ProductDTO>>
                {
                    Message = "Does not have any products!",
                    Data = products
                });
            }
            return Ok(products);
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetProductsNotHidden()
        {
            var products = await _productService.GetProductsNotHidden();
            if (!products.Any())
                return NotFound(new Response<IEnumerable<ProductDTO>>
                {
                    Message = "There are no products available!",
                    Data = products
                });

            return Ok(products);
        }

        [HttpGet("hidden")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> GetProductsHidden()
        {
            var products = await _productService.GetProductsHidden();
            if (!products.Any())
                return NotFound(new Response<IEnumerable<ProductDTO>>
                {
                    Message = "There are no hidden products!",
                    Data = products
                });

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
                return NotFound(new Response<string>
                {
                    Message = "This product does not exist!",
                });
            return Ok(product);
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> UpdateStatusProductById(string id)
        {
            var prodduct = await _productService.GetProductById(id);
            if (prodduct == null)
                return NotFound(new Response<string>
                {
                    Message = "This product does not exist!",
                });

            await _productService.UpdatestatusProduct(id);

            //await _httpClient.PatchAsync($"{api}/{id}", null);
            return Ok(new Response<string>
            {
                Message = "The status is updated successfully"
            });
            
        }

        [HttpPut("update/{id}")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> UpdateInforProduct(string id, [FromForm] RequestProduct requestProduct)
        {
            var prodduct = await _productService.GetProductById(id);
            if (prodduct == null)
                return NotFound();


            Product product = await _productService.UpdateInforProduct(id, requestProduct);

            //await _httpClient.PutAsJsonAsync($"{api}/update", product);


            return Ok(new Response<string>
            {
                Message = "The information is updated successfully"
            });
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productService.DeleteProduct(id);

            //await _httpClient.DeleteAsync($"{api}/delete/{id}");
            return NoContent();
        }
    }
}
