using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeAPI.Core.Entities;
using PracticeAPI.Core.Interface;

namespace EmployeeManagementAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly IDepartment _departmentRepository;

        public DepartmentController(IDepartment departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<Department>>> GetAllDepartments()
        {
            var departments = await _departmentRepository.GetAllDepartmentsAsync();
            return Ok(departments);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> AddDeparment(Employee employee)
        {
            await _departmentRepository.AddDepartmentAsync(employee);
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> UpdateDepartment(Employee employee)
        {
            await _departmentRepository.UpdateDepartmentAsync(employee);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteDepartmentAsync(int id)
        {
            await _departmentRepository.DeleteDepartmentAsync(id);
            return Ok();
        }
    }
}
