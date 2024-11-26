using Microsoft.EntityFrameworkCore;
using Online_Shopping.Context;
using Repository.Contracts.Interfaces;

namespace Repositories.Repositories
{
    public class LoginRepo : ILoginRepo
    {
        private readonly ApplicationContext _applicationContext;

        public LoginRepo(ApplicationContext applicationContext) 
        {
            _applicationContext = applicationContext;
        }

        
        public async Task<string> checkLoginCustomerAsync(string login, string password)
        {
            var customer = await _applicationContext.Customers
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Email == login && u.Password == password);
            if (customer != null)
            {
                return "Customer";
            }
            return null;
        }

        public async Task<string> checkLoginEmployeeAsync(string login, string password)
        {
            var emp = await _applicationContext.Employees
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.Username == login && e.Password == password);
            if (emp != null)
            {
                return emp.RoleId;
            }
            return null;
        }

    }
}
