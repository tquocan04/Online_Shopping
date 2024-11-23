using Entities.Entities;

namespace Repository.Contracts.Interfaces
{
    public interface IUserRepo
    {
        Task<bool> checkUsernameExist(string username);
        Task<bool> checkEmailExist(string email);
        Task<bool> checkDOB(DateOnly dob);
        Task CreateNewCustomer(Customer customer);
        Task CreateNewAddress(Address address);
        Task<Customer?> GetCustomerByIdAsync(Guid id);
        Task<Customer?> GetProfileByCustomerIdIdAsync(Guid id);
        Task UpdateInforCustomer(Customer customer);
        Task CreateAddress(Address address);
        Task<Address?> GetAddressByMultiPKAsync(Guid customerId, Guid districtId, string Street);
        Task<string?> GetStreetDefaultByCustomerIdAsync(Guid customerId);
        Task<Guid> GetDistrictDefaultByCustomerIdAsync(Guid customerId);
        Task DeleteAddress(Address address);
        Task UpdateAddress(Address address);
    }
}
