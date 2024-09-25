using PracticeAPI.Core.Interface;
using PracticeAPI.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace PracticeAPI.Infrastructure.Data.Repository
{
    public class EmployeesRepository : IEmployee
    {
        private readonly EmployeeContext _context;
        public EmployeesRepository(EmployeeContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employee.ToListAsync();
        }
        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employee.FindAsync(id);
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            await _context.Employee.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            _context.Employee.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            if(employee != null)
            {
                _context.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }
        
        public async Task<bool> EmployeeExistsAsync(int id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByDeptAsync(string department)
        {
            return await _context.Employee.Where(e => e.Department == department).ToListAsync();
        }

        public async Task<IEnumerable<Employee>> EmployeeSearchAsync(string searchName)
        {
            var employees = await _context.Employee
                .Where(e => e.Name.Contains(searchName))
                .ToListAsync();

            return employees;
        }

        public async Task<List<Employee>> GetAllEmployeesListAsync()
        {
            return await _context.Employee.ToListAsync();
        }
    }
}
