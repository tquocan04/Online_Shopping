using Entities.Entities;

namespace Repository.Contracts.Interfaces
{
    public interface IUserRepo
    {
        Task<bool> checkUsernameExist(string username);
        Task<bool> checkEmailExist(string email);
        Task<bool> checkDOB(DateOnly dob);
        Task CreateNewUser(User user);
        Task CreateNewCustomer(Customer customer);
        Task CreateNewCusAddress(Cus_Address cus_Address);
        Task<User?> GetUserByIdAsync(Guid id);
        Task<User?> GetProfileByUserIdIdAsync(Guid id);
        Task UpdateInforUser(User user);
        Task CreateCusAddress(Cus_Address cus_Address);
        Task<Cus_Address?> GetCusAddressByMultiPKAsync(Guid userId, Guid districtId, string Street);
        Task<string?> GetStreetDefaultByUserIdAsync(Guid userId);
        Task<Guid> GetDistrictDefaultByUserIdAsync(Guid userId);
        Task DeleteCusAddress(Cus_Address cus_Address);
        Task UpdateCusAddress(Cus_Address cus_Address);
    }
}
