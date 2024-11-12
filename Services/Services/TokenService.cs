﻿using AutoMapper;
using Service.Contracts.Interfaces;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using DTOs.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using DTOs.Request;

namespace Services.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private List<Claim> GetClaim(RequestLogin requestLogin)
        {
            var claims = new List<Claim>    // các thông tin về người dùng sẽ được lưu trong token JWT
            {
                new Claim(ClaimTypes.Name, requestLogin.Email),
                // Add additional claims as needed (Id, email, role,...)
            };
            return claims;
        }

        public string GenerateToken(RequestLogin requestLogin)
        {
            var expiryhour = int.Parse(_configuration["Jwt:TokenExpiredInHour"]);
            
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: GetClaim(requestLogin),
                expires: DateTime.Now.AddHours(expiryhour),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }
    }
}