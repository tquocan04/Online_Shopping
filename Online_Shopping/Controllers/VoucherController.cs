using DTOs.DTOs;
using DTOs.Request;
using DTOs.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts.Interfaces;
using Services.Services;
using System.Net.Http;

namespace Online_Shopping.Controllers
{
    [Route("api/vouchers")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherService _voucherService;
        private readonly HttpClient _httpClient;


        private readonly string api = "http://localhost:5285/api/vouchers/north";

        public VoucherController(IVoucherService voucherService, HttpClient httpClient)
        {
            _voucherService = voucherService;
            _httpClient = httpClient;
        }


        [HttpPost("new-voucher")]
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

            await _httpClient.PostAsJsonAsync($"{api}/new-voucher", voucher);

            return CreatedAtAction(nameof(GetAllVouchers), new { id = voucher.Id }, voucher);
        }

        [HttpGet]
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

            return Ok(new Response<IEnumerable<VoucherDTO>>
            {
                Message = "List vouchers in global",
                Data = list
            });
        }
    }
}
