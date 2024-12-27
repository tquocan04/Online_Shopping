using DTOs.DTOs;
using DTOs.Request;
using DTOs.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.Repositories;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;
using Services.Services;
using System.Net.Http;

namespace Online_Shopping.Controllers
{
    [Route("api/arc-shop")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ARCController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly IEmployeeService _employeeService;
        private readonly ITokenService _tokenService;

        public ARCController(IUserRepo userRepo, 
            ITokenService tokenService,
            IEmployeeService employeeRepo)
        {
            _userRepo = userRepo;
            _employeeService = employeeRepo;
            _tokenService = tokenService;
        }

        [HttpPost("new-staff")]
        public async Task<IActionResult> CreateNewStaff([FromBody] RequestEmployee requestEmployee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            else if (await _userRepo.checkEmailExist(requestEmployee.Email))
            {
                return BadRequest("Email is existed");
            }

            else if (!_userRepo.checkDOB(requestEmployee.Year))
            {
                return BadRequest("DoB is invalid");
            }

            EmployeeDTO empDTO = await _employeeService.AddNewEmployee(requestEmployee);

            return CreatedAtAction("GetProfile", new { id = empDTO.Id }, empDTO);
        }

        [HttpDelete("profile/{employeeId}")]
        public async Task<IActionResult> DeleteStaff(Guid employeeId)
        {
            var emp = await _employeeService.GetProfileEmployee(employeeId);

            if (emp != null)
                await _employeeService.DeleteEmployee(employeeId);

            else
                return NotFound(new Response<string>
                {
                    Message = "This staff does not exist!"
                });

            return NoContent();
        }

        [HttpPut("profile/{employeeId}")]
        public async Task<IActionResult> UpdateProfileStaff(Guid employeeId, [FromBody] RequestEmployee requestEmployee)
        {
            //Admin cap nhat cua Staff
            var check = await _employeeService.UpdateProfile(employeeId, requestEmployee);
            if (!check)
                return BadRequest(new Response<string>
                {
                    Message = "Invalid information!"
                });

            return NoContent();
        }

        [HttpGet("profile/{employeeId}")]
        public async Task<IActionResult> GetProfile(Guid employeeId)
        {
            var profile = await _employeeService.GetProfileEmployee(employeeId);
            if (profile == null)
            {
                return NotFound(new Response<string>
                {
                    Message = "This staff does not exist!"
                });
            }
            return Ok(profile);
        }
    }
}
