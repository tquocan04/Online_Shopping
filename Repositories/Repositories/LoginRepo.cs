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

        public async Task<bool> checkLoginAsync(string email, string password)
        {
            var check = await _applicationContext.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            if (check != null)
            {
                return true;
            }
            return false;
        }

        //public Task<bool> checkPassword(string password)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
