using DTOs.DTOs;
using DTOs.Request;

namespace Service.Contracts.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeDTO> AddNewEmployee(RequestEmployee employee);
        Task DeleteEmployee(string id);
        Task<EmployeeDTO> GetProfileEmployee(string id);
        Task<bool> UpdateProfile(string id, RequestEmployee requestEmployee);
    }
}
