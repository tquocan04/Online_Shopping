using AutoMapper;
using DTOs.DTOs;
using DTOs.Request;
using DTOs.Responses;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;

namespace Online_Shopping.Controllers
{
    [Route("api/vouchers")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherService _voucherService;
        private readonly IVoucherRepo _voucherRepo;
        private readonly IMapper _mapper;

        public VoucherController(IVoucherService voucherService, IVoucherRepo voucherRepo,
            IMapper mapper)
        {
            _voucherService = voucherService;
            _voucherRepo = voucherRepo;
            _mapper = mapper;
        }


        [HttpPost("new-voucher")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateNewVoucher([FromBody] RequestVoucher requestVoucher)
        {
            if (requestVoucher == null)
            {
                return BadRequest(new Response<string>
                {
                    Message = "Invalid information"
                });
            }

            VoucherDTO voucher = await _voucherService.CreateNewVoucher(requestVoucher);

            if(voucher == null)
            {
                return BadRequest(new Response<string>
                {
                    Message = "Cannot create new voucher!!!"
                });
            }

            return CreatedAtAction(nameof(GetAllVouchers), new { id = voucher.Id }, voucher);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllVouchers()
        {
            var list = await _voucherService.GetAllVouchers();
            
            if (!list.Any())
            {
                return NotFound(new Response<string>
                {
                    Message = "Does not have any vouchers!"
                });
            }

            return Ok(list);
        }
        [HttpGet("public")]
        public async Task<IActionResult> GetAllVouchersPublic()
        {
            var list = await _voucherService.GetAllVouchers();

            if (!list.Any())
            {
                return NotFound(new Response<string>
                {
                    Message = "Does not have any vouchers!"
                });
            }

            return Ok(list);
        }

        [HttpGet("detail")]
        [Authorize]
        public async Task<IActionResult> GetDetailVoucher([FromQuery] string code)
        {
            Voucher? voucher = await _voucherRepo.GetDetailVoucherByCode(code);

            if (voucher == null)
                return NotFound(new Response<string>
                {
                    Message = "This voucher does not exist!"
                });

            else if (!voucher.checkExpireDate())
                return BadRequest(new Response<string>
                {
                    Message = "This voucher has expired!"
                });

            else if (voucher.Quantity <= 0)
                return BadRequest(new Response<string>
                {
                    Message = "This voucher is out of stock!"
                });

            return Ok(_mapper.Map<VoucherDTO>(voucher));
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateVoucher(Guid id, [FromBody] RequestVoucher requestVoucher)
        {
            if (requestVoucher == null)
                return NotFound(new Response<string>
                {
                    Message = "Invalid new information!"
                });
            

            var existingvoucher = await _voucherService.GetDetailVoucher(id);
            if (existingvoucher == null)
                return NotFound(new Response<string>
                {
                    Message = "This voucher does not exist to update!"
                });

            Voucher? voucher = await _voucherService.UpdateVoucher(id, requestVoucher);
            if (voucher == null)
                return BadRequest(new Response<string>
                {
                    Message = "Expire time or Code voucher are invalid! Please check again!"
                });

            return Ok(requestVoucher);
        }
    }
}
