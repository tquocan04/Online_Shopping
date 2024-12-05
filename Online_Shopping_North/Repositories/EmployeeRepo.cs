using Microsoft.EntityFrameworkCore;
using Online_Shopping_North.Entities;
using Online_Shopping_North.Repository.Contracts;

namespace Online_Shopping_North.Repositories
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly ApplicationContext _applicationContext;

        public EmployeeRepo(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task AddNewStaff(Employee employee)
        {
            _applicationContext.Employees.Add(employee);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task<Employee?> CheckExistingEmployeeByBranchId(Guid id)
        {
            return await _applicationContext.Employees.AsNoTracking()
                .FirstOrDefaultAsync(e => e.BranchId == id);
        }

        public async Task<bool> CheckUsername(Guid id, string username)
        {
            if (await _applicationContext.Employees.AnyAsync(e => e.Username == username && e.Id != id))
            {
                return false;
            }
            return true;
        }

        public async Task DeleteStaffAsync(Employee employee)
        {
            _applicationContext.Employees.Remove(employee);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task<Employee> GetStaffAsync(Guid id)
        {
            return await _applicationContext.Employees.AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateProfileStaff(Employee employee)
        {
            _applicationContext.Employees.Update(employee);
            await _applicationContext.SaveChangesAsync();
        }
    }
}
