using AutoMapper;
using CloudinaryDotNet.Actions;
using DTOs;
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
using System.Net.Http;
using System.Security.Claims;

namespace Online_Shopping.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        private readonly IOrderRepo _orderRepo;
        private readonly IAddressService<Customer> _addressService;
        private readonly ILoginRepo _loginRepo;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly string api = "http://localhost:5285/api/authentication/north";
        private readonly string apiOrder = "http://localhost:5285/api/orders/north";

        public AuthenticationController(IUserRepo userRepo, IUserService userService,
            HttpClient httpClient, IOrderService orderService, IMapper mapper,
            IAddressService<Customer> addressService, IOrderRepo orderRepo,
            ILoginRepo loginRepo, ITokenService tokenService
            ) 
        {
            _userRepo = userRepo;
            _userService = userService;
            _orderService = orderService;
            _orderRepo = orderRepo;
            _addressService = addressService;
            _loginRepo = loginRepo;
            _tokenService = tokenService;
            _mapper = mapper;
            _httpClient = httpClient;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RequestCustomer requestCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _userRepo.checkEmailExist(requestCustomer.Email))
            {
                return BadRequest("Email is existed");
            }

            if (!_userRepo.checkDOB(requestCustomer.Year))
            {
                return BadRequest("DoB is invalid");
            }

            var newCustomer = await _userService.CreateNewUser(requestCustomer);
            Order order = await _orderService.CreateNewCart(newCustomer.Id);
            
            DistributedCustomer distributedCustomer = new DistributedCustomer
            {
                Id = newCustomer.Id,
                Picture = newCustomer.Picture,
                OrderId = order.Id,
            };

            distributedCustomer = _mapper.Map(requestCustomer, distributedCustomer);

            if (requestCustomer.RegionId == "Bac")
            {
                await _httpClient.PostAsJsonAsync($"{api}/register", distributedCustomer);
                
                await _httpClient.PostAsJsonAsync($"{apiOrder}/{order.Id}/{order.CustomerId}", order);
            }

            return CreatedAtAction("GetProfileUser", new { id = newCustomer.Id }, distributedCustomer);
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateUser([FromForm] RequestCustomer requestCustomer)
        {
            Guid id = await _tokenService.GetEmailCustomerByToken();

            Customer? customer = await _userRepo.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                return NotFound(new Response<string>
                {
                    Message = "This customer does not exist!"
                });
            }

            if (requestCustomer == null)
            {
                return BadRequest(new Response<string>
                {
                    Message = "Invalid information"
                });
            }

            string currentRegionId = await _addressService.GetRegionIdOfObject(customer.Id);

            customer = await _userService.UpdateInforUser(customer, requestCustomer);

            if (customer == null)
                return BadRequest("Cannot update customer");
            

            DistributedCustomer distributedCustomer = new DistributedCustomer
            {
                Id = id,
                Picture = customer.Picture,
            };

            distributedCustomer = _mapper.Map(requestCustomer, distributedCustomer);
            
            if (currentRegionId == "Bac")
            {
                if (requestCustomer.RegionId == "Bac")
                {
                    await _httpClient.PutAsJsonAsync($"{api}/profile", distributedCustomer);
                }
                else
                {
                    await _httpClient.DeleteAsync($"{api}/profile/{customer.Id}");

                }
            }
            else if (currentRegionId != "Bac")
            {
                if (requestCustomer.RegionId == "Bac")
                {
                    await _httpClient.PostAsJsonAsync($"{api}/register", distributedCustomer);
                    var listOrder = await _orderRepo.GetListOrderByCusId(customer.Id);
                    for (int i = 0; i < listOrder.Count(); i++)
                    {
                        await _httpClient.PostAsJsonAsync($"{apiOrder}", listOrder[i]);
                        var listItem = await _orderRepo.GetListItemByOrderId(listOrder[i].Id);
                        if (listItem.Count() != 0)
                        {
                            for (int j = 0; j < listItem.Count(); j++)
                            {
                                await _httpClient.PostAsJsonAsync($"{apiOrder}/item", listItem[j]);
                            }
                        }
                        
                    }
                    return Ok(new Response<string>
                    {
                        Message = "Change orders of this customer successfully"
                    });
                }

            }

            return Ok(new Response<Customer>
            {
                Message = "Customer is updated successfully",
                Data = customer
            });
        }

        [HttpGet("profile")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetProfileUser()
        {
            Guid id = await _tokenService.GetEmailCustomerByToken();

            string regionId = await _addressService.GetRegionIdOfObject(id);
            if (regionId == "Bac")
            {
                var response = await _httpClient.GetAsync($"{api}/profile/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<CustomerDTO>();
                    if (result == null)
                        return NotFound(new Response<string>
                        {
                            Message = "This customer does not exist in North region!"
                        });

                    return Ok(new Response<CustomerDTO>
                    {
                        Message = "This customer is in North region!",
                        Data = result
                    });
                }
            }

            var user = await _userService.GetProfileUser(id.ToString());
            return Ok(user);
        }

    }
}
