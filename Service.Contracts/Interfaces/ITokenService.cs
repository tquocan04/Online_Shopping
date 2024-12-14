﻿using DTOs.DTOs;
using DTOs.Request;

namespace Service.Contracts.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(RequestLogin requestLogin, string role);
        Task<Guid> GetEmailCustomerByToken();
        Task<Guid> GetIdEmployeeByToken();
    }
}
