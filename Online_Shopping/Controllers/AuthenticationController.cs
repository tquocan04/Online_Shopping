using AutoMapper;
using CloudinaryDotNet.Actions;
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
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        private readonly IOrderRepo _orderRepo;
        private readonly IAddressService<Customer> _addressService;
        private readonly ITokenService _tokenService;
        private readonly ICredentialRepo _credentialRepo;
        private readonly IMapper _mapper;

        public AuthenticationController(IUserRepo userRepo, IUserService userService,
            IOrderService orderService, IMapper mapper,
            IAddressService<Customer> addressService, IOrderRepo orderRepo,
            ITokenService tokenService,
            ICredentialRepo credentialRepo
            ) 
        {
            _userRepo = userRepo;
            _userService = userService;
            _orderService = orderService;
            _orderRepo = orderRepo;
            _addressService = addressService;
            _tokenService = tokenService;
            _credentialRepo = credentialRepo;
            _mapper = mapper;
        }
        [HttpPost("signup-google")]
        public async Task<IActionResult> SignUpGG([FromBody] RequestSignupGoogle requestSignupGoogle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _userRepo.checkEmailExist(requestSignupGoogle.email))
            {
                return BadRequest("Email is existed");
            }

            if (!_userRepo.checkDOB(requestSignupGoogle.year))
            {
                return BadRequest("DoB is invalid");
            }

            var newCustomer = await _userService.CreateNewUserByGoogle(requestSignupGoogle);
            Order order = await _orderService.CreateNewCart(newCustomer.Id);

            Credential credential = new()
            {
                Id = requestSignupGoogle.googleId,
                Provider = "Google",
                CustomerId = newCustomer.Id,
            };
            await _credentialRepo.CreateCredentialAsync(credential);

            DistributedCustomer distributedCustomer = new()
            {
                Id = newCustomer.Id,
                OrderId = order.Id,
            };

            distributedCustomer = _mapper.Map(requestSignupGoogle, distributedCustomer);

            return CreatedAtAction("GetProfileUser", new { id = newCustomer.Id }, distributedCustomer);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RequestCustomer requestCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _userRepo.checkEmailExist(requestCustomer.Email))
            {
                return BadRequest("Email is existed");
            }

            if (!_userRepo.checkDOB(requestCustomer.Year))
            {
                return BadRequest("DoB is invalid");
            }

            var newCustomer = await _userService.CreateNewUser(requestCustomer);
            Order order = await _orderService.CreateNewCart(newCustomer.Id);
                
            DistributedCustomer distributedCustomer = new()
            {
                Id = newCustomer.Id,
                Picture = newCustomer.Picture,
                OrderId = order.Id,
            };

            distributedCustomer = _mapper.Map(requestCustomer, distributedCustomer);

            return CreatedAtAction("GetProfileUser", new { id = newCustomer.Id }, distributedCustomer);
        }

        [HttpPut("profile")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UpdateUser([FromForm] RequestCustomer requestCustomer)
        {
            Guid id = await _tokenService.GetIdCustomerByToken();

            Customer? customer = await _userRepo.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                return NotFound(new Response<string>
                {
                    Message = "This customer does not exist!"
                });
            }

            if (requestCustomer == null)
            {
                return BadRequest(new Response<string>
                {
                    Message = "Invalid information"
                });
            }

            customer = await _userService.UpdateInforUser(customer, requestCustomer);

            if (customer == null)
                return BadRequest("Cannot update customer");
            
            return Ok(new Response<Customer>
            {
                Message = "Customer is updated successfully",
                Data = customer
            });
        }

        [HttpGet("profile")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetProfileUser()
        {
            Guid id = await _tokenService.GetIdCustomerByToken();

            if (id == Guid.Empty)
                return NotFound(new Response<string>
                {
                    Message = "Does not exist customer."
                });

            return Ok(new Response<CustomerDTO>
            {
                Message = "Profile customer.",
                Data = await _userService.GetProfileUser(id)
            });            
        }

        [HttpGet("email")]
        public async Task<IActionResult> CheckEmailExist([FromQuery] string email)
        {
            if (await _userRepo.checkEmailExist(email))
                return Ok(new Response<string>
                {
                    Message = "Valid email."
                });

            return BadRequest(new Response<string>
            {
                Message = "Invalid email!"
            });
        }

        [HttpPatch("new-password")]
        public async Task<IActionResult> UpdateNewPassword([FromBody] RequestLogin login)
        {
            if (!await _userRepo.checkEmailExist(login.Login))
                return BadRequest(new Response<string>
                {
                    Message = "Invalid email!"
                });
            if (login.Password != null)
            {
                await _userRepo.UpdateNewPassword(login.Login, login.Password);
                return Ok(new Response<string>
                {
                    Message = "Updated password successfully!"
                });
            }
            return BadRequest(new Response<string>
            {
                Message = "New password is null!"
            });
        }
    }
}
