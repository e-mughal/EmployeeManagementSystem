using Microsoft.EntityFrameworkCore;
using PracticeAPI.Core.Entities;
using PracticeAPI.Core.Interface;

namespace PracticeAPI.Infrastructure.Data.Repository
{
    public class DepartmentsRepository : IDepartment
    {
        private readonly EmployeeContext _context;

        public DepartmentsRepository(EmployeeContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task AddDepartmentAsync(Employee employee)
        {
            var department = new Department
            {
                DeptName = employee.Department,
                Employee = employee.Name
            };

            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDepartmentAsync(Employee employee)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(e => e.DeptId == employee.Id);
            if (department != null)
            {
                department.DeptName = employee.Department;
                department.Employee = employee.Name;

                _context.Departments.Update(department);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if(department != null)
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
            }
        }
    }
}
