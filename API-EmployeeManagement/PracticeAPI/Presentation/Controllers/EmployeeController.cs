using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeAPI.Core.Interface;
using PracticeAPI.Infrastructure.Data.Repository;
using PracticeAPI.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace PracticeAPI.Presentation.Controllers
{
    // API Controller class for Employees
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        // Creating variables to access employee and department repo functions
        private readonly IEmployee _employeesRepository;
        private readonly IDepartment _departmentRepository;

        /*
         * Constructor
         * param: employee repo var
         * param: department repo var
         * 
         * Initializes the above variables.
         */
        public EmployeeController(IEmployee employeesRepository, IDepartment departmentRepository)
        {
            _employeesRepository = employeesRepository;
            _departmentRepository = departmentRepository;
        }

        /*
         * Getting all the employees
         * 
         * Calls the employee repo function, returning an enumerable of all employees
         */
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            var employees = await _employeesRepository.GetAllEmployeesAsync();
            return Ok(employees);
        }

        /*
         * Getting all the employees
         * 
         * Calls the employee repo function, returning a list of all employees
         */
        [HttpGet]
        [Route("List/")]
        public async Task<ActionResult<List<Employee>>> GetAllEmployeesListAsync()
        {
            var employees = await _employeesRepository.GetAllEmployeesListAsync();
            return Ok(employees);
        }

        /*
         * Checks to see if an employee exists
         * param: employee id
         * 
         * Calls the employee repo function, returning the appropriate response.
         */
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

        /*
         * Gets employee by their id
         * param: employee id
         * 
         * Calls the employee repo function, checking to see if an employee exists.
         */
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

        /*
         * Adds an employee to the database.
         * param: employee entity
         * 
         * Ensures employee is not null, then adds them to the employee and department tables accordingly.
         */
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

        /*
         * Updates the employee in the database
         * param: employee id
         * param: updated employee entity
         * 
         * Checks to see if employee id matches given id, then updates employee in both employee and department tables
         */
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

        /*
         * Deletes an employee from the database
         * param: employee id
         * 
         * Calls the employee and deparment repo functions, deleting the employee from the database, if the employee exists at the given id.
         */
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

        /*
         * Gets all employees in a department
         * param: department name -> string
         * 
         * Checks if string is valid, and then calls employee repo function, returning accordingly.
         */
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

        /*
         * Gets employees based on search
         * param: a search term -> string
         * 
         * Checks if the search is valid, and returns accordingly.
         */
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
