using EmployeeManagement.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Application.Interface;


namespace EmployeeManagement.Web.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartment _departmentService;
        private readonly IEmployee _employeeService;

        public DepartmentsController(IDepartment departmentService, IEmployee employeeService)
        {
            _departmentService = departmentService;
            _employeeService = employeeService;
        }

        /*public async Task<IActionResult> Index()
        {
            var departments = await _departmentRepository.GetAllDepartmentsAsync();
            return View(departments);
        }*/

        public async Task<IActionResult> Index(string department)
        {
            if (string.IsNullOrEmpty(department))
            {
                return RedirectToAction("Index");
            }

            var employees = await _employeeService.GetEmployeesByDeptAsync(department);
            ViewBag.Department = department;
            ViewBag.PageTitle = $"{department}";
            ViewBag.CardHeader = "Employees";
            return View(employees);
        }
    }
}
