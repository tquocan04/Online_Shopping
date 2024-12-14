using AutoMapper;
using DTOs.Request;
using DTOs.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;

namespace Online_Shopping.Controllers
{
    [Route("api/bills")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IPaymentRepo _paymentRepo;
        private readonly ITokenService _tokenService;
        private readonly IBillService _billService;
        private readonly IShippingRepo _shippingRepo;
        private readonly IBillRepo _billRepo;
        private readonly IMapper _mapper;
        //private readonly HttpClient _httpClient;

        ////private readonly string apiOrder = "http://localhost:5285/api/orders/north";


        public BillController(IOrderService orderService,
            IPaymentRepo paymentRepo,
            IMapper mapper, IBillService billService,
            IShippingRepo shippingRepo,
            IBillRepo billRepo,
            HttpClient httpClient, ITokenService tokenService)
        {
            _orderService = orderService;
            _paymentRepo = paymentRepo;
            _tokenService = tokenService;
            _billService = billService;
            _shippingRepo = shippingRepo;
            _billRepo = billRepo;
            _mapper = mapper;
            //_httpClient = httpClient;
        }
        
        [HttpPut("payment")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> PaytoBill([FromBody] RequestBill requestBill)
        {
            if (await _paymentRepo.GetPaymentIdAsync(requestBill.PaymentId) == null)
                return BadRequest(new Response<string>
                {
                    Message = "Does not exist this payment!"
                });

            if (await _shippingRepo.GetShippingMethodByIdAsync(requestBill.ShippingMethodId) == null)
                return BadRequest(new Response<string>
                {
                    Message = "Does not exist this payment!"
                });


            Guid id = await _tokenService.GetEmailCustomerByToken();
            if (id == Guid.Empty)
                return BadRequest(new Response<string>
                {
                    Message = "Customer is invalid!"
                });

            var order = await _billService.CartToBill(id, requestBill);


            if (order == null)
                return BadRequest(new Response<string>
                {
                    Message = "Cannot create this bill!"
                });
            //if (currentRegion == "Bac")
            //{
            //    await _httpClient.DeleteAsync($"{apiOrder}/delete-item/{id}/{prodId}");
            //}


            return Ok(order);
        }

        [HttpGet()]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetBills()
        {

            Guid id = await _tokenService.GetEmailCustomerByToken();
            if (id == Guid.Empty)
                return BadRequest(new Response<string>
                {
                    Message = "Customer is invalid!"
                });

            var listBills = await _billService.GetOrderBill(id);

            if (listBills == null)
                return NotFound(new Response<string>
                {
                    Message = "Does not have any bills!"
                });

            return Ok(listBills);
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> CompletedBills(Guid id)
        {
            await _billRepo.CompletedBillAsync(id);

            return NoContent();
        }

        [HttpGet("pending-bills")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> GetListPendingBills()
        {
            var list = await _billService.GetListPendingBill();
            if (list == null)
                return NotFound(new Response<string>
                {
                    Message = "Does not have any pending bills!"
                });

            return Ok(list);
        }
        
        [HttpGet("completed-bills")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> GetListCompletedBills()
        {
            var list = await _billService.GetListCompletedBill();
            if (list == null)
                return NotFound(new Response<string>
                {
                    Message = "Does not have any completed bills!"
                });

            return Ok(list);
        }
        
        [HttpGet("detail/{id}")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> GetDetailBills(Guid id)
        {
            var bill = await _billService.GetBillDetail(id);

            if (bill == null)
                return NotFound(new Response<string>
                {
                    Message = "Does not have any completed bills!"
                });

            return Ok(bill);
        }
    }
}
