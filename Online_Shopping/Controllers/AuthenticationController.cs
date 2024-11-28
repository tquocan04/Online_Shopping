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

        public AuthenticationController(IUserRepo userRepo, IUserService userService,
            IMapper mapper
            ) 
        {
            _userRepo = userRepo;
            _userService = userService;
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

            return CreatedAtAction("GetProfileUser", new { id = newCustomer.Id }, newCustomer);
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
