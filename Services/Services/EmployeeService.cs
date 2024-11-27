using AutoMapper;
using DTOs.DTOs;
using DTOs.Request;
using Entities.Entities;
using Repository.Contracts.Interfaces;
using Service.Contracts.Interfaces;

namespace Services.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepo _employeeRepo;
        private readonly IMapper _mapper;
        private readonly IAddressRepo _addressRepo;
        private readonly IBranchRepo _branchRepo;
        private readonly IAddressService<EmployeeDTO> _addressService;

        public EmployeeService(IEmployeeRepo employeeRepo, IMapper mapper, IAddressRepo addressRepo,
            IBranchRepo branchRepo, IAddressService<EmployeeDTO> addressService) 
        {
            _employeeRepo = employeeRepo;
            _mapper = mapper;
            _addressRepo = addressRepo;
            _branchRepo = branchRepo;
            _addressService = addressService;
        }

        public async Task<EmployeeDTO> AddNewEmployee(RequestEmployee employee)
        {
            Employee emp = new Employee
            {
                Id = new Guid(),
                Dob = new DateOnly(employee.Year, employee.Month, employee.Day)
            };
            _mapper.Map(employee, emp);
            
            await _employeeRepo.AddNewStaff(emp);


            Address address = new Address
            {
                ObjectId = emp.Id,
                DistrictId = employee.DistrictId,
                Street = employee.Street,
                IsDefault = true
            };

            await _addressRepo.CreateNewAddress(address);
            

            return _mapper.Map<EmployeeDTO>(emp);
        }

        public async Task DeleteEmployee(string id)
        {
            var emp = await _employeeRepo.GetStaffAsync(Guid.Parse(id));

            var address = await _addressRepo.GetAddressByObjectIdAsync(emp.Id);

            await _addressRepo.DeleteAddress(address);
            await _employeeRepo.DeleteStaffAsync(emp);
        }

        public async Task<EmployeeDTO> GetProfileEmployee(string id)
        {
            var emp = await _employeeRepo.GetStaffAsync(Guid.Parse(id));
            var empDTO = _mapper.Map<EmployeeDTO>(emp);

            empDTO = await _addressService.SetAddress(empDTO, empDTO.Id);

            var branch = await _branchRepo.GetBranchAsync(empDTO.BranchId);
            empDTO.BranchName = branch.Name;

            return empDTO;
        }
    }
}
