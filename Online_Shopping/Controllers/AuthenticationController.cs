using AutoMapper;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
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
        private readonly Cloudinary _cloudinary;

        public AuthenticationController(IUserRepo userRepo, IUserService userService,
            IMapper mapper, IOrderRepo orderRepo, IAddressRepo addressRepo,
            Cloudinary cloudinary
            ) 
        {
            _userRepo = userRepo;
            _userService = userService;
            _mapper = mapper;
            _orderRepo = orderRepo;
            _addressRepo = addressRepo;
            _cloudinary = cloudinary;
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

            Customer customer = new Customer
            {
                Id = new Guid(),
                Dob = new DateOnly(requestCustomer.Year, requestCustomer.Month, requestCustomer.Day)
            };

            // Xử lý ảnh và tải lên Cloudinary nếu có
            if (requestCustomer.Picture != null && requestCustomer.Picture.Length > 0)
            {
                await _userService.UploadImage(customer, requestCustomer.Picture);
            }

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
            };

            await _orderRepo.CreateOrder(order);
            return CreatedAtAction("Register", new { id = customer.Id }, customer);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromForm] RequestCustomer requestCustomer)
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
