using DTOs.DTOs;
using DTOs.Request;
using Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace Service.Contracts.Interfaces
{
    public interface IUserService
    {
        Task<Customer> CreateNewUser(RequestCustomer requestUser);
        
        Task<Customer> UpdateInforUser(Customer customer, RequestCustomer requestUser);
        Task<CustomerDTO> GetProfileUser(string userId);

        Task UploadImage(Customer customer, IFormFile file);
    }
}
