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
            if (await _applicationContext.Customers.AnyAsync(u => u.Username == username))
            {
                return true;
            }
            return false;
        }
        
        public async Task<bool> checkEmailExist(string email)
        {
            if (await _applicationContext.Customers.AnyAsync(u => u.Email == email))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> checkDOB(DateOnly dob)
        {
            if (await _applicationContext.Customers.AnyAsync(u => u.Dob.Year < DateTime.Now.Year))
            {
                return true;
            }
            return false;
        }

        public async Task CreateNewCustomer(Customer customer)
        {
            await _applicationContext.Customers.AddAsync(customer);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task CreateNewAddress(Address address)
        {
            await _applicationContext.Addresses.AddAsync(address);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(Guid id)
        {
            return await _applicationContext.Customers.FindAsync(id);
        }

        public async Task UpdateInforCustomer(Customer customer)
        {
            _applicationContext.Customers.Update(customer);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task DeleteAddress(Address address)
        {
            _applicationContext.Addresses.Remove(address);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task CreateAddress(Address address)
        {
            try
            {
                _applicationContext.Addresses.Add(address);
                await _applicationContext.SaveChangesAsync();
            }
            catch 
            {
                throw new Exception("Cannot update Address");
            }
        }

        public async Task<Address?> GetAddressByMultiPKAsync(Guid customerId, Guid districtId, string Street)
        {
            return await _applicationContext.Addresses
                .FirstOrDefaultAsync(ca => ca.ObjectId == customerId
                                            &&  ca.DistrictId == districtId
                                            && ca.Street == Street);
        }

        public async Task<string?> GetStreetDefaultByCustomerIdAsync(Guid customerId)
        {
            var address =  await _applicationContext.Addresses
                .FirstOrDefaultAsync(ca => ca.ObjectId == customerId && ca.IsDefault);

            return address?.Street;
        }

        public async Task<Guid> GetDistrictDefaultByCustomerIdAsync(Guid customerId)
        {
            var address = await _applicationContext.Addresses
                .AsNoTracking()
                .FirstOrDefaultAsync(ca => ca.ObjectId == customerId && ca.IsDefault);

            return address.DistrictId;
        }

        public async Task UpdateAddress(Address cus_Address)
        {
            _applicationContext.Addresses.Update(cus_Address);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task<Customer> GetProfileByCustomerIdIdAsync(Guid id)
        {
            //var districtId = await GetDistrictDefaultByUserIdAsync(id);
            //var street = 
            return await _applicationContext.Customers.FindAsync(id);
        }
    }
}
