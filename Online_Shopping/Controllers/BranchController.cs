using AutoMapper;
using DTOs.DTOs;
using DTOs.Request;
using DTOs.Responses;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.Repositories;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;
using Services.Services;

namespace Online_Shopping.Controllers
{
    [Route("api/branches")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchService;
        private readonly IMapper _mapper;
        private readonly IAddressRepo _addressRepo;
        private readonly IAddressService<Branch> _addressService;

        public BranchController(IBranchService branchService, IMapper mapper,
            IAddressService<Branch> addressService,
            IAddressRepo addressRepo)
        {
            _branchService = branchService;
            _mapper = mapper;
            _addressRepo = addressRepo;
            _addressService = addressService;
        }

        [HttpPost("new-branch")]
        public async Task<IActionResult> AddNewBranch([FromBody] RequestBranch requestBranch)
        {
            BranchDTO branchDTO = await _branchService.AddNewBranch(requestBranch);

            return CreatedAtAction(nameof(GetBranch), new { id = branchDTO.Id }, requestBranch);
        }

        [HttpGet]
        //[Authorize]
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

            await _branchService.DeleteBranch(id);
            
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

            return NoContent();
        }
    }
}
