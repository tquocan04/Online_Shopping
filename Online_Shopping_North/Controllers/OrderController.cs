using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Shopping_North.DTOs;
using Online_Shopping_North.Entities;
using Online_Shopping_North.Repository.Contracts;
using Online_Shopping_North.Service.Contracts;

namespace Online_Shopping_North.Controllers
{
    [Route("api/orders/north")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IOrderRepo _orderRepo;

        public OrderController(IOrderService orderService, IOrderRepo orderRepo)
        {
            _orderService = orderService;
            _orderRepo = orderRepo;
        }

        [HttpGet("cart/{cusId}")]
        //[Authorize]
        public async Task<IActionResult> GetCart(Guid cusId)
        {
            OrderCartDTO cart = await _orderService.GetOrderCart(cusId);
            return Ok(cart);
        }

        [HttpPost("new-item/{cusId}/{prodId}")]
        //[Authorize]
        public async Task<IActionResult> AddToCart(Guid cusId, Guid prodId)
        {
            await _orderService.AddToCart(cusId, prodId);
            return NoContent();
        }

        [HttpDelete("delete-item/{cusId}/{prodId}")]
        public async Task<IActionResult> DeleteItemInCart(Guid cusId, Guid prodId)
        {
            await _orderService.DeleteItemInCart(cusId, prodId);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            await _orderRepo.CreateOrder(order);
            return CreatedAtAction(nameof(CreateOrder), new { id = order.Id });
        }
        
        [HttpPost("{id}/{cusId}")]
        public async Task<IActionResult> RegisterOrder(Guid id, Guid cusId)
        {
            Order order = new Order
            {
                Id = id,
                CustomerId = cusId,
                TotalPrice = 0,
            };
            await _orderRepo.CreateOrder(order);
            return CreatedAtAction(nameof(RegisterOrder), new { id = order.Id });
        }

        [HttpPost("item")]
        public async Task<IActionResult> CreateItem([FromBody] Item item)
        {
            await _orderRepo.AddItemToCart(item);
            return CreatedAtAction(nameof(CreateItem), new { id = item.OrderId });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        { 
            await _orderRepo.DeleteOrderAsync(id);
            return NoContent();
        }
    }
}
