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
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        

        public OrderController(IOrderService orderService) 
        {
            _orderService = orderService;
            
        }

        [HttpGet("cart")]
        //[Authorize]
        public async Task<IActionResult> GetCart(string cusId) 
        {
            var cart = await _orderService.GetOrderCartAsync(cusId);
            return Ok(cart);
        }

        [HttpPost("add-to-cart")]
        //[Authorize]
        public async Task<IActionResult> AddToCart(string cusId, string prodId)
        {
            await _orderService.AddToCartAsync(cusId, prodId);
            return NoContent();
        }

        [HttpDelete("delete-item")]
        public async Task<IActionResult> DeleteItemInCart(string cusId, string prodId)
        {
            await _orderService.DeleteItemInCartAsync(cusId, prodId);
            return NoContent();
        }
    }
}
