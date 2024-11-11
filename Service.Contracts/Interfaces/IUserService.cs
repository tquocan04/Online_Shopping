using DTOs.Request;
using Entities.Entities;

namespace Service.Contracts.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateNewUser(RequestUser requestUser);
        //Task CreateNewCustomer(User user);
        Task<bool> UpdateInforUser(string id, string districtId, RequestUser requestUser);
    }
}
