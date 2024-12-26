using AutoMapper;
using DTOs;
using DTOs.Request;
using DTOs.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Repositories;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;
using Services.Services;

namespace Online_Shopping.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly ITokenService _tokenService;
        private readonly ICredentialRepo _credentialRepo;
        private readonly IUserRepo _userRepo;

        public LoginController(ILoginService loginService, ITokenService tokenService,
            ICredentialRepo credentialRepo, IUserRepo userRepo) 
        {
            _loginService = loginService;
            _tokenService = tokenService;
            _credentialRepo = credentialRepo;
            _userRepo = userRepo;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] RequestLogin requestLogin)
        {
            if (requestLogin == null)
                return BadRequest("Please fill in all login information");

            if (requestLogin.Login.Contains('@'))
            {
                Guid customerId = await _userRepo.GetCustomerIdByEmailAsync(requestLogin.Login);

                string idgg = await _credentialRepo.GetCredentialIdByCustomerIdAsync(customerId);

                if (idgg != null)
                    return BadRequest(new Response<string>
                    {
                        Message = "Please login by google with this email!"
                    });
            }

            var result = await _loginService.LoginAsync(requestLogin);
            string role = result.Item1;
            string? picture = result.Item2;
            if (role == null)
                return BadRequest("Account is wrong! Please try again");
            var token = _tokenService.GenerateToken(requestLogin, role);
            
            return Ok(new AuthResponse
            {
                AccessToken = $"Bearer {token}",
                Picture = picture,
            });
        }

        [HttpPost("login-google")]
        public async Task<IActionResult> LoginByGoogle([FromQuery] string idGG)
        {
            Guid customerId = await _credentialRepo.GetCustomerIdAsync(idGG);
            if (customerId == Guid.Empty)
                return BadRequest(new Response<string>
                {
                    Message = "Invalid GoogleId!"
                });
            
            string email = await _userRepo.GetEmailByCustomerIdAsync(customerId);
            if (email == null)
                return BadRequest(new Response<string>
                {
                    Message = "Invalid CustomerId!"
                });

            RequestLogin login = new()
            {
                Login = email,
                Password = null
            };

            var result = await _loginService.LoginAsync(login);
            string role = result.Item1;
            string? picture = result.Item2;
            if (role == null)
                return BadRequest("Account is wrong! Please try again");
            var token = _tokenService.GenerateToken(login, role);

            return Ok(new AuthResponse
            {
                AccessToken = $"Bearer {token}",
                Picture = picture,
            });
        }
    }
}