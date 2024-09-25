using PracticeAPI.Core.Entities;

namespace PracticeAPI.Core.Interface
{
    public interface IDepartment
    {
        Task<IEnumerable<Department>> GetAllDepartmentsAsync();
        Task AddDepartmentAsync(Employee employee);
        Task UpdateDepartmentAsync(Employee employee);
        Task DeleteDepartmentAsync(int id);
    }
}
