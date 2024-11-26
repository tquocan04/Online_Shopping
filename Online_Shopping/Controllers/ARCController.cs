﻿using AutoMapper;
using DTOs.DTOs;
using DTOs.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;

namespace Online_Shopping.Controllers
{
    [Route("api/arc-shop")]
    [ApiController]
    public class ARCController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IAddressRepo _addressRepo;
        private readonly IEmployeeService _employeeService;

        public ARCController(IUserRepo userRepo, IUserService userService,
            IMapper mapper, IAddressRepo addressRepo,
            IEmployeeService employeeRepo)
        {
            _userRepo = userRepo;
            _userService = userService;
            _mapper = mapper;
            _addressRepo = addressRepo;
            _employeeService = employeeRepo;
        }

        [HttpPost("add-new-staff")]
        public async Task<IActionResult> CreateNewStaff([FromBody] RequestEmployee requestEmployee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _userRepo.checkEmailExist(requestEmployee.Email))
            {
                return BadRequest("Email is existed");
            }

            if (!_userRepo.checkDOB(requestEmployee.Year))
            {
                return BadRequest("DoB is invalid");
            }
            EmployeeDTO empDTO = await _employeeService.AddNewEmployee(requestEmployee);
            return CreatedAtAction("CreateNewStaff", new { id = empDTO.Id }, empDTO);
        }

        [HttpDelete("delete-staff/{id}")]
        public async Task<IActionResult> DeleteStaff(string id)
        {
            await _employeeService.DeleteEmployee(id);
            return NoContent();
        }

        [HttpGet("profile/{id}")]
        public async Task<IActionResult> GetProile(string id)
        {
            var profile = await _employeeService.GetProfileEmployee(id);
            return Ok(profile);
        }
    }
}
