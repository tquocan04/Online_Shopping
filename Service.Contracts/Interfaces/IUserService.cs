using DTOs.DTOs;
using DTOs.Request;
using Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace Service.Contracts.Interfaces
{
    public interface IUserService
    {
        Task<Customer> CreateNewUser(Guid Id, RequestCustomer requestUser);
        //Task CreateNewCustomer(User user);
        Task<bool> UpdateInforUser(string id, RequestCustomer requestUser);
        Task<CustomerDTO> GetProfileUser(string userId);

        Task UploadImage(Customer customer, IFormFile file);
    }
}
