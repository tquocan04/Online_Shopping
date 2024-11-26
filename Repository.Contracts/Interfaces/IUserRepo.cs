﻿using Entities.Entities;

namespace Repository.Contracts.Interfaces
{
    public interface IUserRepo
    {
        Task<bool> checkEmailExist(string email);
        public bool checkDOB(int year);
        Task CreateNewCustomer(Customer customer);
        Task<Customer?> GetCustomerByIdAsync(Guid id);
        Task<Customer?> GetProfileByCustomerIdIdAsync(Guid id);
        Task UpdateInforCustomer(Customer customer);
        Task<string?> GetStreetDefaultByCustomerIdAsync(Guid customerId);
        Task<Guid> GetDistrictDefaultByCustomerIdAsync(Guid customerId);
    }
}
