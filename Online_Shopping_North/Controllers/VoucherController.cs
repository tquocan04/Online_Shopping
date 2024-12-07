using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Shopping_North.DTOs;
using Online_Shopping_North.Responses;
using Online_Shopping_North.Service.Contracts;

namespace Online_Shopping_North.Controllers
{
    [Route("api/vouchers/north")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherService _voucherService;

        public VoucherController(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }

        [HttpPost("new-voucher")]
        public async Task<IActionResult> CreateNewVoucher([FromBody] VoucherDTO voucherDTO)
        {
            await _voucherService.CreateNewVoucher(voucherDTO);


            return CreatedAtAction(nameof(CreateNewVoucher), new { id = voucherDTO.Id });
        }
    }
}
