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

namespace Online_Shopping.Controllers
{
    [Route("api/branches")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchService;
        private readonly IMapper _mapper;
        private readonly IAddressRepo _addressRepo;

        public BranchController(IBranchService branchService, IMapper mapper,
            IAddressRepo addressRepo)
        {
            _branchService = branchService;
            _mapper = mapper;
            _addressRepo = addressRepo;
        }

        [HttpPost("add-new-branch")]
        public async Task<IActionResult> AddNewBranch([FromBody] RequestBranch requestBranch)
        {
            BranchDTO branchDTO = await _branchService.AddNewBranch(requestBranch);

            Address address = new Address
            {
                ObjectId = branchDTO.Id,
                IsDefault = true,
            };
            _mapper.Map(requestBranch, address);
            await _addressRepo.CreateNewAddress(address);
            return CreatedAtAction(nameof(GetAllBranches), new { id = branchDTO.Id }, requestBranch);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBranches()
        {
            var list = await _branchService.GetBranchList();
            if (list == null)
                return NotFound();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBranch(string id)
        {
            var branch = await _branchService.GetBranch(id);
            if (branch == null)
                return NotFound();
            return Ok(branch);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBranch(string id)
        {
            await _branchService.DeleteBranch(id);
            return NoContent();
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateBranch(string id, [FromBody] RequestBranch requestBranch)
        {
            await _branchService.UpdateBranch(id, requestBranch);
            return NoContent();
        }
    }
}
