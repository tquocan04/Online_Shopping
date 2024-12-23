using AutoMapper;
using DTOs.DTOs;
using DTOs.Request;
using DTOs.Responses;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Repositories;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;
using Services.Services;

namespace Online_Shopping.Controllers
{
    [Route("api/orders")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IAddressService<Customer> _addressService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        
        public OrderController(IOrderService orderService,
            IAddressService<Customer> addressService,
            IMapper mapper, ITokenService tokenService) 
        {
            _orderService = orderService;
            _addressService = addressService;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        
        [HttpGet("cart")]
        public async Task<IActionResult> GetCart() 
        {
            Guid id = await _tokenService.GetIdCustomerByToken();
            if (id == Guid.Empty)
                return BadRequest(new Response<string>
                {
                    Message = "Customer is invalid!"
                });

            var cart = await _orderService.GetOrderCart(id);
            return Ok(cart);
        }

        [HttpPost("new-item")]
        public async Task<IActionResult> AddToCart([FromQuery] Guid prodId)
        {
            Guid id = await _tokenService.GetIdCustomerByToken();

            if (id == Guid.Empty)
                return BadRequest(new Response<string>
                {
                    Message = "Customer is invalid!"
                });

            await _orderService.AddToCart(id, prodId);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteItemInCart([FromQuery] Guid prodId)
        {
            Console.WriteLine($"product controller: {prodId}");
            Guid id = await _tokenService.GetIdCustomerByToken();
            
            await _orderService.DeleteItemInCart(id, prodId);
            
            return NoContent();
        }

        [HttpPost("all-items")]
        public async Task<IActionResult> AddProducts([FromBody] List<RequestItems> products)
        {
            Guid id = await _tokenService.GetIdCustomerByToken();

            if (id == Guid.Empty)
                return BadRequest(new Response<string>
                {
                    Message = "Customer is invalid!"
                });

            if (products == null || !products.Any())
            {
                return BadRequest(new Response<string>
                {
                    Message = "Product list is empty."
                });
            }

            OrderCartDTO cart = await _orderService.MergeCartFromClient(id, products);
            if (cart == null)
                return BadRequest(new Response<string>
                {
                    Message = "Exist one product do not have in database."
                });


            return Ok(new Response<OrderCartDTO>
            {
                Message = "Products is merged successfully.",
                Data = cart
            });

        }

        [HttpPatch("{productId}")]
        public async Task<IActionResult> UpdateQuantityItem(Guid productId, [FromQuery] int Quantity)
        {
            Guid id = await _tokenService.GetIdCustomerByToken();

            if (id == Guid.Empty)
                return BadRequest(new Response<string>
                {
                    Message = "Customer is invalid!"
                });

            bool check = await _orderService.UpdateQuantityItem(id, productId, Quantity);
            if (!check)
                return BadRequest(new Response<string>
                {
                    Message = "Insufficient quantity of product!"
                });

            return NoContent();
        }
    }
}