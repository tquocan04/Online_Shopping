using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Online_Shopping.Context;
using Repository.Contracts.Interfaces;

namespace Repositories.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly ApplicationContext _applicationContext;

        public UserRepo(ApplicationContext applicationContext) 
        {
            _applicationContext = applicationContext;
        }

        public async Task<bool> checkUsernameExist(string username)
        {
            if (await _applicationContext.Users.AnyAsync(u => u.Username == username))
            {
                return true;
            }
            return false;
        }
        
        public async Task<bool> checkEmailExist(string email)
        {
            if (await _applicationContext.Users.AnyAsync(u => u.Email == email))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> checkDOB(DateOnly dob)
        {
            if (await _applicationContext.Users.AnyAsync(u => u.Dob.Year < DateTime.Now.Year))
            {
                return true;
            }
            return false;
        }

        public async Task CreateNewUser(User user)
        {
            await _applicationContext.Users.AddAsync(user);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task CreateNewCustomer(Customer customer)
        {
            await _applicationContext.Customers.AddAsync(customer);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task CreateNewCusAddress(Cus_Address cus_Address)
        {
            await _applicationContext.CusAddresses.AddAsync(cus_Address);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _applicationContext.Users.FindAsync(id);
        }

        public async Task UpdateInforUser(User user)
        {
            _applicationContext.Users.Update(user);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task DeleteCusAddress(Cus_Address cus_Address)
        {
            _applicationContext.CusAddresses.Remove(cus_Address);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task CreateCusAddress(Cus_Address cus_Address)
        {
            try
            {
                _applicationContext.CusAddresses.Add(cus_Address);
                await _applicationContext.SaveChangesAsync();
            }
            catch 
            {
                throw new Exception("Cannot update Cus_Address");
            }
        }

        public async Task<Cus_Address?> GetCusAddressByMultiPKAsync(Guid userId, Guid districtId, string Street)
        {
            return await _applicationContext.CusAddresses
                .FirstOrDefaultAsync(ca => ca.UserId == userId
                &&  ca.DistrictId == districtId
                && ca.Street == Street);
        }

        public async Task<string?> GetStreetDefaultByUserIdAsync(Guid userId)
        {
            var cus_Address =  await _applicationContext.CusAddresses
                .FirstOrDefaultAsync(ca => ca.UserId == userId && ca.IsDefault);

            return cus_Address?.Street;
        }

        public async Task<Guid> GetDistrictDefaultByUserIdAsync(Guid userId)
        {
            var cus_Address = await _applicationContext.CusAddresses
                .FirstOrDefaultAsync(ca => ca.UserId == userId && ca.IsDefault);

            return cus_Address.DistrictId;
        }

        public async Task UpdateCusAddress(Cus_Address cus_Address)
        {
            //Cus_Address cus = new Cus_Address();

            //_applicationContext.CusAddresses.Entry(cus).CurrentValues.SetValues(cus_Address);
            _applicationContext.CusAddresses.Update(cus_Address);
            await _applicationContext.SaveChangesAsync();
        }
    }
}
