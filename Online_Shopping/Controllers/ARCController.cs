using AutoMapper;
using DTOs.DTOs;
using DTOs.Request;
using DTOs.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;
using System.Net.Http;

namespace Online_Shopping.Controllers
{
    [Route("api/arc-shop")]
    [ApiController]
    public class ARCController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly IEmployeeService _employeeService;
        private readonly HttpClient _httpClient;

        private readonly string api = "http://localhost:5285/api/arc-shop/north";

        public ARCController(IUserRepo userRepo, 
            HttpClient httpClient,
            IEmployeeService employeeRepo)
        {
            _userRepo = userRepo;
            _employeeService = employeeRepo;
            _httpClient = httpClient;
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

            if (requestEmployee.RegionId == "Bac")
            {
                await _httpClient.PostAsJsonAsync($"{api}/new-staff/{empDTO.Id}", requestEmployee);
            }
            return CreatedAtAction("GetProile", new { id = empDTO.Id }, empDTO);
        }

        [HttpDelete("profile/{id}")]
        public async Task<IActionResult> DeleteStaff(string id)
        {
            var emp = await _employeeService.GetProfileEmployee(id);
            
            if (emp != null)
            {
                await _employeeService.DeleteEmployee(id);

                if (emp.RegionId == "Bac")
                {
                    await _httpClient.DeleteAsync($"{api}/profile/{id}");
                }
            }
            else
            {
                return NotFound(new Response<string>
                {
                    Message = "This staff does not exist!"
                });
            }

            return NoContent();
        }
        
        [HttpPut("profile/{id}")]
        public async Task<IActionResult> UpdateProfileStaff(string id, [FromBody] RequestEmployee requestEmployee)
        {
            var check = await _employeeService.UpdateProfile(id, requestEmployee);
            if (!check)
            {
                return BadRequest(new Response<string>
                {
                    Message = "Invalid information!"
                });
            }
            else
            {
                if (requestEmployee.RegionId == "Bac")
                {
                    await _httpClient.PutAsJsonAsync($"{api}/profile/{id}", requestEmployee);
                }
            }
            return NoContent();
        }

        [HttpGet("profile/{id}")]
        public async Task<IActionResult> GetProfile(string id)
        {
            var profile = await _employeeService.GetProfileEmployee(id);
            if (profile == null)
            {
                return NotFound(new Response<string>
                {
                    Message = "This staff does not exist!"
                });
            }
            else
            {
                if (profile.RegionId == "Bac")
                {
                    var response = await _httpClient.GetAsync($"{api}/profile/{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        return Ok(await response.Content.ReadFromJsonAsync<EmployeeDTO>());
                    }
                }
            }
            return Ok(profile);
        }
    }
}
