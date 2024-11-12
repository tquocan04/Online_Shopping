﻿using AutoMapper;
using DTOs;
using DTOs.Request;
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
        public async Task<IActionResult> Register(string districtId, [FromBody] RequestUser requestUser)
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

            DateOnly dob = new DateOnly(requestUser.Year, requestUser.Month, requestUser.Day);
            if (!await _userRepo.checkDOB(dob))
            {
                return BadRequest("DoB is invalid");
            }
            var user = await _userService.CreateNewUser(requestUser);


            Customer customer = new Customer
            {
                Id = new Guid(),
                UserId = user.Id,
            };
            await _userRepo.CreateNewCustomer(customer);


            Cus_Address cus_Address = new Cus_Address 
            { 
                UserId = user.Id, 
                IsDefault = true,
                DistrictId = Guid.Parse(districtId),
            };

            _mapper.Map(requestUser, cus_Address);

            await _userRepo.CreateNewCusAddress(cus_Address);

            return CreatedAtAction("Register", new { id = user.Id }, user);
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
    }
}