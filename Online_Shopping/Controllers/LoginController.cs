using DTOs;
using DTOs.Request;
using DTOs.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public LoginController(ILoginService loginService, ITokenService tokenService) 
        {
            _loginService = loginService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] RequestLogin requestLogin)
        {
            if(!ModelState.IsValid)
                return BadRequest("Please fill in all login information");

            var check = await _loginService.LoginAsync(requestLogin);
            if (!check)
                return BadRequest("Email or password is wrong! Please try again");
            var token = _tokenService.GenerateToken(requestLogin);
            return Ok(new AuthResponse
            {
                AccessToken = $"Bearer {token}"
            });
        }
    }
}