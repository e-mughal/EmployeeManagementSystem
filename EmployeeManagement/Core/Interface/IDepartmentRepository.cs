using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Domain.Interface
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllDepartmentsAsync();
        Task AddDepartmentAsync(Employee employee);
        Task UpdateDepartmentAsync(Employee employee);
        Task DeleteDepartmentAsync(int id);
    }
}
