using DTOs.DTOs;
using DTOs.Request;
using Entities.Entities;

namespace Service.Contracts.Interfaces
{
    public interface IUserService
    {
        Task<Customer> CreateNewUser(Guid Id, RequestCustomer requestUser);
        //Task CreateNewCustomer(User user);
        Task<bool> UpdateInforUser(string id, string districtId, RequestCustomer requestUser);
        Task<CustomerDTO> GetProfileUser(string userId);
    }
}
