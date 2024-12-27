using DTOs.DTOs;
using DTOs.Request;
using DTOs.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;

namespace Online_Shopping.Controllers
{
    [Route("api/arc-shop")]
    [ApiController]
    [Authorize(Roles = "Admin, Staff")]
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

        [HttpDelete("profile")]
        public async Task<IActionResult> DeleteStaff()
        {
            Guid id = await _tokenService.GetIdEmployeeByToken();
            var emp = await _employeeService.GetProfileEmployee(id);

            if (emp != null)
                await _employeeService.DeleteEmployee(id);

            else
                return NotFound(new Response<string>
                {
                    Message = "This staff does not exist!"
                });

            return NoContent();
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfileStaff([FromBody] RequestEmployee requestEmployee)
        {
            Guid id = await _tokenService.GetIdEmployeeByToken();
            
            var check = await _employeeService.UpdateProfile(id, requestEmployee);
            if (!check)
                return BadRequest(new Response<string>
                {
                    Message = "Invalid information!"
                });

            return NoContent();
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            Guid id = await _tokenService.GetIdEmployeeByToken();
            var profile = await _employeeService.GetProfileEmployee(id);
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
