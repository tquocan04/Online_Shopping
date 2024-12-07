using DTOs.DTOs;
using DTOs.Responses;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Repositories;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;
using Services.Services;
using System.Net.Http;

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
        private readonly HttpClient _httpClient;

        private readonly string apiOrder = "http://localhost:5285/api/orders/north";

        

        public OrderController(IOrderService orderService, IAddressService<Customer> addressService,
            HttpClient httpClient, ITokenService tokenService) 
        {
            _orderService = orderService;
            _addressService = addressService;
            _tokenService = tokenService;
            _httpClient = httpClient;
        }
        
        [HttpGet("cart")]
        public async Task<IActionResult> GetCart() 
        {
            Guid id = await _tokenService.GetEmailCustomerByToken();
            string currentRegion = await _addressService.GetRegionIdOfObject(id);
            if (currentRegion == "Bac")
            {
                var response = await _httpClient.GetAsync($"{apiOrder}/cart/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return Ok(new Response<OrderCartDTO>{
                        Message = "Cart from customer in North",
                        Data = await response.Content.ReadFromJsonAsync<OrderCartDTO>()
                    });
                }

            }
            var cart = await _orderService.GetOrderCart(id.ToString());
            return Ok(cart);
        }

        [HttpPost("new-item")]
        public async Task<IActionResult> AddToCart(string prodId)
        {
            Guid id = await _tokenService.GetEmailCustomerByToken();
            string currentRegion = await _addressService.GetRegionIdOfObject(id);
            if (currentRegion == "Bac")
            {
                await _httpClient.PostAsync($"{apiOrder}/new-item/{id}/{prodId}", null);

            }
            await _orderService.AddToCart(id.ToString(), prodId);
            return NoContent();
        }

        [HttpDelete("delete-item")]
        public async Task<IActionResult> DeleteItemInCart(string prodId)
        {
            Guid id = await _tokenService.GetEmailCustomerByToken();
            string currentRegion = await _addressService.GetRegionIdOfObject(id);
            if (currentRegion == "Bac")
            {
                await _httpClient.DeleteAsync($"{apiOrder}/delete-item/{id}/{prodId}");
            }
            
            await _orderService.DeleteItemInCart(id.ToString(), prodId);
            
            return NoContent();
        }
    }
}
