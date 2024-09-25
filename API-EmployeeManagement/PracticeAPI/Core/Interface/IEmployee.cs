using PracticeAPI.Core.Entities;

namespace PracticeAPI.Core.Interface
{
    public interface IEmployee
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<List<Employee>> GetAllEmployeesListAsync();
        Task AddEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(int id);
        Task<bool> EmployeeExistsAsync(int id);
        Task<IEnumerable<Employee>> GetEmployeesByDeptAsync(string department);
        Task<IEnumerable<Employee>> EmployeeSearchAsync(string searchName);

    }
}
