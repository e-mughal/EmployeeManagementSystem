using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Interface
{
    public interface IDepartment
    {
        Task<IEnumerable<Department>> GetAllDepartmentsAsync();
        Task AddDepartmentAsync(Employee employee);
        Task UpdateDepartmentAsync(Employee employee);
        Task DeleteDepartmentAsync(int id);
    }
}
