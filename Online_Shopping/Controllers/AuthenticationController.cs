using AutoMapper;
using CloudinaryDotNet.Actions;
using DTOs;
using DTOs.DTOs;
using DTOs.Request;
using DTOs.Responses;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.Repositories;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;
using Services.Services;

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

        public AuthenticationController(IUserRepo userRepo, IUserService userService,
            IOrderService orderService, IMapper mapper,
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

            return CreatedAtAction("GetProfileUser", new { id = newCustomer.Id }, distributedCustomer);
        }

        [HttpPut("profile")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UpdateUser([FromForm] RequestCustomer requestCustomer)
        {
            Guid id = await _tokenService.GetIdCustomerByToken();

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

            customer = await _userService.UpdateInforUser(customer, requestCustomer);

            if (customer == null)
                return BadRequest("Cannot update customer");
            
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
            Guid id = await _tokenService.GetIdCustomerByToken();
            
            return Ok(await _userService.GetProfileUser(id));
        }

    }
}
