using Online_Shopping_North.Entities;

namespace Online_Shopping_North.Repository.Contracts
{
    public interface ICustomerRepo
    {
        Task<bool> checkEmailExist(string email);
        Task<bool> checkEmailById(Guid id, string email);
        public bool checkDOB(int year);
        Task CreateNewCustomer(Customer customer);
        Task<Customer?> GetCustomerByIdAsync(Guid id);
        Task<Customer?> GetProfileByCustomerIdIdAsync(Guid id);
        Task UpdateInforCustomer(Customer customer);
        Task<string?> GetStreetDefaultByCustomerIdAsync(Guid customerId);
        Task<Guid> GetDistrictDefaultByCustomerIdAsync(Guid customerId);
        Task DeleteCustomerAsync(Customer customer);
    }
}
