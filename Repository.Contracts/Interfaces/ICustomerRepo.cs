using Entities.Entities;

namespace Repository.Contracts.Interfaces
{
    public interface ICustomerRepo
    {
        Task<Customer> GetCustomerIdAsync(Guid userId);
    }
}
