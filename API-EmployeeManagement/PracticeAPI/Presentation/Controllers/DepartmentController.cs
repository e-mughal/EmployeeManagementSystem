using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeAPI.Core.Entities;
using PracticeAPI.Core.Interface;

namespace EmployeeManagementAPI.Presentation.Controllers
{
    // API Controller class for Employee Departments
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        // Creating a variable to access information from the department repo, ensuring it remains constant
        private readonly IDepartment _departmentRepository;

        /*
         * Constructor
         * param: department repo var
         * 
         * Initializes the department repo variable we created above
         * 
         */
        public DepartmentController(IDepartment departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        /*
         * Gets an Enumerable of all Departments.
         * 
         * Calls the department repo function, returns accordingly.
         */
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<Department>>> GetAllDepartments()
        {
            var departments = await _departmentRepository.GetAllDepartmentsAsync();
            return Ok(departments);
        }

        /*
         * Adds an employee to the department table in the database
         * param: employee entity
         * 
         * Calls the department repo function, and returns accordingly.
         */
        [HttpPost]
        [Route("")]
        public async Task<ActionResult> AddDeparment(Employee employee)
        {
            await _departmentRepository.AddDepartmentAsync(employee);
            return Ok();
        }

        /*
         * Updates an employees department in the database
         * param: employee
         * 
         * Calls the department repo function, and returns accordingly.
         */
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> UpdateDepartment(Employee employee)
        {
            await _departmentRepository.UpdateDepartmentAsync(employee);
            return Ok();
        }

        /*
         * Deletes a department from the database
         * param: department id
         * 
         */
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteDepartmentAsync(int id)
        {
            await _departmentRepository.DeleteDepartmentAsync(id);
            return Ok();
        }
    }
}
