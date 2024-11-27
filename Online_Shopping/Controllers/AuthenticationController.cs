using AutoMapper;
using DTOs;
using DTOs.DTOs;
using DTOs.Request;
using DTOs.Responses;
using Entities.Entities;
using Microsoft.AspNetCore.Http;
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
        private readonly IMapper _mapper;
        private readonly IOrderRepo _orderRepo;
        private readonly IAddressRepo _addressRepo;

        public AuthenticationController(IUserRepo userRepo, IUserService userService,
            IMapper mapper, IOrderRepo orderRepo, IAddressRepo addressRepo
            ) 
        {
            _userRepo = userRepo;
            _userService = userService;
            _mapper = mapper;
            _orderRepo = orderRepo;
            _addressRepo = addressRepo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RequestCustomer requestCustomer)
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

            Customer customer = new Customer
            {
                Id = new Guid(),
                Dob = new DateOnly(requestCustomer.Year, requestCustomer.Month, requestCustomer.Day)
            };
            _mapper.Map(requestCustomer, customer);
            await _userRepo.CreateNewCustomer(customer);

            Address address = new Address 
            { 
                ObjectId = customer.Id,
                IsDefault = true,
            };
            _mapper.Map(requestCustomer, address);
            await _addressRepo.CreateNewAddress(address);

            Order order = new Order
            {
                Id = new Guid(),
                CustomerId = customer.Id,
                TotalPrice = 0,
                //OrderDate = DateTime.Now,
            };

            await _orderRepo.CreateOrder(order);
            return CreatedAtAction("Register", new { id = customer.Id }, customer);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] RequestCustomer requestCustomer)
        {
            var check = await _userService.UpdateInforUser(id, requestCustomer);
            if (!check)
                return BadRequest("Cannot update user");

            return Ok(new Response<RequestCustomer>
            {
                Message = "User is updated successfully",
                Data = requestCustomer
            });
        }

        [HttpGet("profile/{Id}")]
        public async Task<IActionResult> GetProfileUser(string Id)
        {
            var user = await _userService.GetProfileUser(Id);
            return Ok(user);
        }

    }
}
