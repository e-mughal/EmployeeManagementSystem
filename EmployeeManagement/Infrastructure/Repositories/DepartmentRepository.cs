using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interface;
using EmployeeManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly EmployeeManagementContext _context;

        public DepartmentRepository(EmployeeManagementContext context)
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
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.DeptId == employee.Id);
            if(department != null)
            {
                department.Employee = employee.Name;
                department.DeptName = employee.Department;

                _context.Departments.Update(department);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
            }
        }
    }
}
