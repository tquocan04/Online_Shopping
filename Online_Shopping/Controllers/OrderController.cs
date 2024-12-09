using AutoMapper;
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
        private readonly IPaymentRepo _paymentRepo;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        //private readonly string apiOrder = "http://localhost:5285/api/orders/north";

        

        public OrderController(IOrderService orderService,
            IPaymentRepo paymentRepo,
            IAddressService<Customer> addressService,
            IMapper mapper,
            HttpClient httpClient, ITokenService tokenService) 
        {
            _orderService = orderService;
            _addressService = addressService;
            _paymentRepo = paymentRepo;
            _tokenService = tokenService;
            _mapper = mapper;
            _httpClient = httpClient;
        }
        
        [HttpGet("cart")]
        //[Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetCart() 
        {
            Guid id = await _tokenService.GetEmailCustomerByToken();
            if (id == Guid.Empty)
                return BadRequest(new Response<string>
                {
                    Message = "Customer is invalid!"
                });

            string currentRegion = await _addressService.GetRegionIdOfObject(id);
            //if (currentRegion == "Bac")
            //{
            //    var response = await _httpClient.GetAsync($"{apiOrder}/cart/{id}");
            //    if (response.IsSuccessStatusCode)
            //    {
            //        return Ok(new Response<OrderCartDTO>{
            //            Message = "Cart from customer in North",
            //            Data = await response.Content.ReadFromJsonAsync<OrderCartDTO>()
            //        });
            //    }

            //}
            var cart = await _orderService.GetOrderCart(id.ToString());
            return Ok(cart);
        }

        [HttpPost("new-item")]
        //[Authorize(Roles = "Customer")]
        public async Task<IActionResult> AddToCart(string prodId)
        {
            Guid id = await _tokenService.GetEmailCustomerByToken();

            if (id == Guid.Empty)
                return BadRequest(new Response<string>
                {
                    Message = "Customer is invalid!"
                });

            string currentRegion = await _addressService.GetRegionIdOfObject(id);
            //if (currentRegion == "Bac")
            //{
            //    await _httpClient.PostAsync($"{apiOrder}/new-item/{id}/{prodId}", null);

            //}
            await _orderService.AddToCart(id.ToString(), prodId);
            return NoContent();
        }

        [HttpDelete("delete-item")]
        public async Task<IActionResult> DeleteItemInCart(string prodId)
        {
            Guid id = await _tokenService.GetEmailCustomerByToken();
            string currentRegion = await _addressService.GetRegionIdOfObject(id);
            //if (currentRegion == "Bac")
            //{
            //    await _httpClient.DeleteAsync($"{apiOrder}/delete-item/{id}/{prodId}");
            //}
            
            await _orderService.DeleteItemInCart(id.ToString(), prodId);
            
            return NoContent();
        }

        [HttpPut("pay/{paymentId}/{shippingId}")]
        //[Authorize(Roles = "Customer")]
        public async Task<IActionResult> PaytoBill(string paymentId, string shippingId, 
            [FromQuery] string? voucherCode)
        {
            if (await _paymentRepo.GetPaymentIdAsync(paymentId) == null)
                return BadRequest(new Response<string>
                {
                    Message = "Does not exist this payment!"
                });
            
            if (await _paymentRepo.GetPaymentIdAsync(paymentId) == null)
                return BadRequest(new Response<string>
                {
                    Message = "Does not exist this payment!"
                });


            Guid id = await _tokenService.GetEmailCustomerByToken();
            if (id == Guid.Empty)
                return BadRequest(new Response<string>
                {
                    Message = "Customer is invalid!"
                });

            var order = await _orderService.CartToBill(id, paymentId, shippingId, voucherCode);


            if (order == null)
                return BadRequest(new Response<string>
                {
                    Message = "Cannot create this bill!"
                });
            //if (currentRegion == "Bac")
            //{
            //    await _httpClient.DeleteAsync($"{apiOrder}/delete-item/{id}/{prodId}");
            //}


            return Ok(order);
        }

        [HttpGet("bills")]
        //[Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetBills()
        {
            
            Guid id = await _tokenService.GetEmailCustomerByToken();
            if (id == Guid.Empty)
                return BadRequest(new Response<string>
                {
                    Message = "Customer is invalid!"
                });
            
            var listBills = await _orderService.GetOrderBill(id);

            if (listBills.Count == 0)
                return NotFound(new Response<string>
                {
                    Message = "Does not have any bills!"
                });

            return Ok(listBills);
        }
    }
}
