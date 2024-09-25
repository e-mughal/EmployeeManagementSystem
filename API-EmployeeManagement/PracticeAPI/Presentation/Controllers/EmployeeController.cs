using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeAPI.Core.Interface;
using PracticeAPI.Infrastructure.Data.Repository;
using PracticeAPI.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace PracticeAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employeesRepository;
        private readonly IDepartment _departmentRepository;

        public EmployeeController(IEmployee employeesRepository, IDepartment departmentRepository)
        {
            _employeesRepository = employeesRepository;
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            var employees = await _employeesRepository.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet]
        [Route("List/")]
        public async Task<ActionResult<List<Employee>>> GetAllEmployeesListAsync()
        {
            var employees = await _employeesRepository.GetAllEmployeesListAsync();
            return Ok(employees);
        }

        [HttpGet]
        [Route("exist/{id}")]
        public async Task<ActionResult<bool>> EmployeeExists(int id)
        {
            var exist = await _employeesRepository.EmployeeExistsAsync(id);
            if (exist)
            {
                return Ok(true);
            }
            return Ok(false);
        }

        [HttpGet]
        [Route("id/{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var exist = await _employeesRepository.EmployeeExistsAsync(id);
            if(!exist)
            {
                return BadRequest("ERROR! Employee with provided ID does not exist in the database");
;            }
            var employee = await _employeesRepository.GetEmployeeByIdAsync(id);
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee([Bind("Id,Name,Email,DateOfBirth,Department")] Employee employee)
        {
            if(employee == null)
            {
                return BadRequest("ERROR! Employee Data is NULL");
            }
            
            await _employeesRepository.AddEmployeeAsync(employee);
            await _departmentRepository.AddDepartmentAsync(employee);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> UpdateEmployee(int id, [Bind("Id,Name,Email,DateOfBirth,Department")] Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest("ERROR! ID Missmatch");
            }

            try
            {
                await _employeesRepository.UpdateEmployeeAsync(employee);
                await _departmentRepository.UpdateDepartmentAsync(employee);
            }
            catch (DbUpdateConcurrencyException)
            {
                var condition = await _employeesRepository.EmployeeExistsAsync(id);
                if (!condition)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            var employee = await _employeesRepository.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return BadRequest("ERROR! This Employee does not exist in the database.");
            }
            
            await _employeesRepository.DeleteEmployeeAsync(id);
            await _departmentRepository.DeleteDepartmentAsync(id);

            return NoContent();
        }

        [HttpGet]
        [Route("department/{department}")]
        public async Task<ActionResult<Employee>> GetEmployeesByDept(string department)
        {
            if(string.IsNullOrEmpty(department))
            {
                return NotFound();
            }
            var employees = await _employeesRepository.GetEmployeesByDeptAsync(department);
            return Ok(employees);
        }

        [HttpGet]
        [Route("{searchName}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesBySearch(string searchName)
        {
            if(string.IsNullOrEmpty(searchName))
            {
                var all = GetAllEmployees();
                return Ok(all);
            }
            var employees = await _employeesRepository.EmployeeSearchAsync(searchName);
            return Ok(employees);
        }
    }
}
