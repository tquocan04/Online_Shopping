﻿using Entities.Entities;

namespace Repository.Contracts.Interfaces
{
    public interface IEmployeeRepo
    {
        Task AddNewStaff(Employee employee);
        Task DeleteStaffAsync(Employee employee);
        Task<Employee> GetStaffAsync(Guid id);
    }
}
