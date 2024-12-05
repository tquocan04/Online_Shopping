using AutoMapper;
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
using System.Net.Http;

namespace Online_Shopping.Controllers
{
    [Route("api/branches")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchService;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly IAddressRepo _addressRepo;

        private readonly string api = "http://localhost:5285/api/branches/north";

        public BranchController(IBranchService branchService, IMapper mapper,
            HttpClient httpClient,
            IAddressRepo addressRepo)
        {
            _branchService = branchService;
            _mapper = mapper;
            _httpClient = httpClient;
            _addressRepo = addressRepo;
        }

        [HttpPost("new-branch")]
        public async Task<IActionResult> AddNewBranch([FromBody] RequestBranch requestBranch)
        {
            BranchDTO branchDTO = await _branchService.AddNewBranch(requestBranch);
            await _httpClient.PostAsJsonAsync($"{api}/new-branch/{branchDTO.Id}", requestBranch);

            return CreatedAtAction(nameof(GetAllBranches), new { id = branchDTO.Id }, requestBranch);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBranches()
        {
            var list = await _branchService.GetBranchList();
            if (list.Count == 0)
            {
                return NotFound(new Response<List<BranchDTO>>
                {
                    Message = "Don't have any branches!",
                    Data = list
                });
            }
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBranch(string id)
        {
            var branch = await _branchService.GetBranch(id);
            
            if (branch == null)
            {
                return NotFound(new Response<string>
                {
                    Message = "This branch does not exist!"
                });
            }

            if (branch.RegionId == "Bac")
            {
                var response = await _httpClient.GetAsync($"{api}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return Ok(await response.Content.ReadFromJsonAsync<BranchDTO>());
                }
            }

            return Ok(branch);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBranch(string id)
        {
            var branch = await _branchService.GetBranch(id);
            if (branch == null)
            {
                return NotFound(new Response<string>
                {
                    Message = "This branch does not exist!"
                });
            }
            else
            {
                await _branchService.DeleteBranch(id);
                if (branch.RegionId == "Bac")
                {
                    await _httpClient.DeleteAsync($"{api}/{id}");
                }
            }
            
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBranch(string id, [FromBody] RequestBranch requestBranch)
        {
            if (await _branchService.GetBranch(id) == null)
            {
                return NotFound(new Response<string>
                {
                    Message = "This branch does not exist!"
                });
            }

            await _branchService.UpdateBranch(id, requestBranch);

            if (requestBranch.RegionId == "Bac")
            {
                await _httpClient.PutAsJsonAsync($"{api}/{id}", requestBranch);
            }

            return NoContent();
        }
    }
}
