﻿using Entities.Entities;
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
        
        public async Task<bool> checkEmailExist(string email)
        {
            if (await _applicationContext.Customers.AnyAsync(u => u.Email == email))
            {
                return true;
            }
            return false;
        }

        public bool checkDOB(int year)
        { 
            if (year < DateTime.Now.Year)
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

        public async Task<Customer?> GetCustomerByIdAsync(Guid id)
        {
            return await _applicationContext.Customers.FindAsync(id);
        }

        public async Task UpdateInforCustomer(Customer customer)
        {
            _applicationContext.Customers.Update(customer);
            await _applicationContext.SaveChangesAsync();
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

        public async Task<Customer> GetProfileByCustomerIdIdAsync(Guid id)
        {
            return await _applicationContext.Customers.FindAsync(id);
        }

    }
}
