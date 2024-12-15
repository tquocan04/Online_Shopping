﻿using AutoMapper;
using DTOs.DTOs;
using DTOs.Request;
using Entities.Entities;
using Repository.Contracts.Interfaces;
using Service.Contracts;
using Service.Contracts.Interfaces;
using System.Net;
using System.Runtime.InteropServices;

namespace Services.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepo _employeeRepo;
        private readonly IMapper _mapper;
        private readonly IAddressRepo _addressRepo;
        private readonly IBranchRepo _branchRepo;
        private readonly IAddressService<EmployeeDTO> _addressService;
        private readonly IUserRepo _userRepo;

        public EmployeeService(IEmployeeRepo employeeRepo, IMapper mapper, 
            IAddressRepo addressRepo,
            IBranchRepo branchRepo, 
            IAddressService<EmployeeDTO> addressService,
            IUserRepo userRepo)
        {
            _employeeRepo = employeeRepo;
            _mapper = mapper;
            _addressRepo = addressRepo;
            _branchRepo = branchRepo;
            _addressService = addressService;
            _userRepo = userRepo;
        }

        public async Task<EmployeeDTO> AddNewEmployee(RequestEmployee employee)
        {
            Employee emp = new Employee
            {
                Id = Guid.NewGuid(),
                Dob = new DateOnly(employee.Year, employee.Month, employee.Day)
            };
            _mapper.Map(employee, emp);

            await _employeeRepo.AddNewStaff(emp);
            

            Address address = new Address
            {
                Id = Guid.NewGuid(),
                EmployeeId = emp.Id,
                IsDefault = true
            };
            _mapper.Map(employee, address);

            await _addressRepo.CreateNewAddress(address);
            var empDTO = _mapper.Map<EmployeeDTO>(emp);
            empDTO = await _addressService.SetAddress(empDTO, empDTO.Id);

            return empDTO;
        }

        public async Task DeleteEmployee(Guid id)
        {
            var emp = await _employeeRepo.GetStaffAsync(id);

            await _employeeRepo.DeleteStaffAsync(emp);
        }

        public async Task<EmployeeDTO> GetProfileEmployee(Guid id)
        {
            var emp = await _employeeRepo.GetStaffAsync(id);

            if (emp == null)
            {
                return null;
            }    
            var empDTO = _mapper.Map<EmployeeDTO>(emp);
            empDTO.Year = emp.Dob.Year;
            empDTO.Month = emp.Dob.Month;
            empDTO.Day = emp.Dob.Day;

            var nameParts = emp.Name.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (nameParts.Length > 0)
            {
                empDTO.FirstName = nameParts[0]; // Từ đầu tiên
                empDTO.LastName = nameParts[nameParts.Length - 1]; // Từ cuối cùng

                if (nameParts.Length > 2)
                {
                    empDTO.MiddleName = string.Join(" ", nameParts.Skip(1).Take(nameParts.Length - 2));
                }
            }
            empDTO = await _addressService.SetAddress(empDTO, empDTO.Id);

            var branch = await _branchRepo.GetBranchAsync(empDTO.BranchId);
            if (branch != null)
            {
                empDTO.BranchName = branch.Name;
            }

            return empDTO;
        }

        public async Task<bool> UpdateProfile(Guid id, RequestEmployee requestEmployee)
        {
            var emp = await _employeeRepo.GetStaffAsync(id);
            if (emp == null)
            {
                throw new ArgumentNullException("User cannot be found");
                return false;
            }
            if (requestEmployee == null)
            {
                throw new ArgumentNullException("Information cannot be null");
                return false;
            }

            var existingAddress = await _addressRepo.GetAddressByObjectIdAsync(id);

            if (existingAddress != null)
            {
                if (existingAddress.DistrictId != requestEmployee.DistrictId || existingAddress.Street != requestEmployee.Street)
                {
                    _mapper.Map(requestEmployee, existingAddress);
                    await _addressRepo.UpdateAddress(existingAddress);
                }

            }
                

            DateOnly dob = new DateOnly(requestEmployee.Year, requestEmployee.Month, requestEmployee.Day);
            if (!_userRepo.checkDOB(requestEmployee.Year))
            {
                throw new Exception("Dob is invalid");
                return false;
            }

            if (!await _employeeRepo.CheckUsername(emp.Id, requestEmployee.Username))
            {
                return false;
            }

            _mapper.Map(requestEmployee, emp);

            emp.Dob = dob;
            await _employeeRepo.UpdateProfileStaff(emp);

            return true;
        }
    }
}