using AutoMapper;
using DTOs;
using DTOs.Request;
using DTOs.Responses;
using Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public AuthenticationController(IUserRepo userRepo, IUserService userService,
            IMapper mapper) 
        {
            _userRepo = userRepo;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RequestUser requestUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _userRepo.checkEmailExist(requestUser.Email))
            {
                return BadRequest("Email is existed");
            }

            if (await _userRepo.checkUsernameExist(requestUser.Username))
            {
                return BadRequest("Username is existed");
            }

            if (!_userRepo.checkDOB(requestUser.Year))
            {
                return BadRequest("DoB is invalid");
            }

            Customer customer = new Customer
            {
                Id = new Guid(),
                Dob = new DateOnly(requestUser.Year, requestUser.Month, requestUser.Day)
            };
            _mapper.Map(requestUser, customer);
            await _userRepo.CreateNewCustomer(customer);

            Address address = new Address 
            { 
                ObjectId = customer.Id,
                IsDefault = true,
            };

            _mapper.Map(requestUser, address);

            await _userRepo.CreateNewAddress(address);

            return CreatedAtAction("Register", new { id = customer.Id }, customer);
        }

        [HttpPut("update-information-user/id/{userId}")]
        public async Task<IActionResult> UpdateUser(string userId, string districtId, [FromBody] RequestUser requestUser)
        {
            var check = await _userService.UpdateInforUser(userId, districtId, requestUser);
            if (!check)
                return BadRequest("Cannot update user");

            return Ok(new Response<RequestUser>
            {
                Message = "User is updated successfully",
                Data = requestUser
            });
        }

        [HttpGet("get-profile/id/{Id}")]
        public async Task<IActionResult> GetProfileUser(string Id)
        {
            var user = await _userService.GetProfileUser(Id);
            return Ok(user);
        }
    }
}
