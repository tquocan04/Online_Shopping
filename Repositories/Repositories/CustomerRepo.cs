using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Online_Shopping.Context;
using Repository.Contracts.Interfaces;

namespace Repositories.Repositories
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly ApplicationContext _applicationContext;

        public CustomerRepo(ApplicationContext applicationContext) 
        {
            _applicationContext = applicationContext;
        }
        public async Task<Customer> GetCustomerIdAsync(Guid customerId)
        {
            return await _applicationContext.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == customerId);
        }
    }
}
