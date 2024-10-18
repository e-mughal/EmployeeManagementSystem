using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Infrastructure.Data;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Application.Interface;


namespace EmployeeManagement.Web.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployee _employeeService;
        private readonly IDepartment _departmentService;

        public EmployeesController(IEmployee employeeService, IDepartment departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }

        // GET: Employees
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            ViewBag.PageTitle = "All Employees";
            ViewBag.CardHeader = "Table";
            ViewBag.Department = "Employees";

            return View(employees);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string searchName)
        {
            if(string.IsNullOrEmpty(searchName))
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.PageTitle = "All Employees";
            ViewBag.CardHeader = "Table";
            ViewBag.Department = "Employees";

            var employees = await _employeeService.EmployeeSearchAsync(searchName);

            return View(employees);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if(employee == null)
            {
                return NotFound();
            }

            ViewBag.PageTitle = "Employee";
            ViewBag.CardHeader = "Details";
            ViewBag.Department = "Details";

            return View(employee);

        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,DateOfBirth,Department")] Employee employee)
        {
            ViewBag.PageTitle = "Employee";
            ViewBag.CardHeader = "Create";
            ViewBag.Department = "Create";
            if (ModelState.IsValid)
            {
                await _employeeService.AddEmployeeAsync(employee);
                await _departmentService.AddDepartmentAsync(employee);

                TempData["newUser"] = true;
                return RedirectToAction(nameof(Index));
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewBag.PageTitle = "Employee";
            ViewBag.CardHeader = "Edit";
            ViewBag.Department = "Edit";
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,DateOfBirth,Department")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _employeeService.UpdateEmployeeAsync(employee);

                    await _departmentService.UpdateDepartmentAsync(employee);
                }
                catch (DbUpdateConcurrencyException)
                {
                    var condition = await _employeeService.EmployeeExistsAsync(employee.Id);
                    if (!condition)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                ViewBag.PageTitle = "Employee";
                ViewBag.CardHeader = "Edit";
                ViewBag.Department = "Edit";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.PageTitle = "Employee";
            ViewBag.CardHeader = "Edit";
            ViewBag.Department = "Edit";
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewBag.PageTitle = "Employee";
            ViewBag.CardHeader = "Delete";
            ViewBag.Department = "Delete";

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id);

            await _departmentService.DeleteDepartmentAsync(id);
           
            return RedirectToAction(nameof(Index));
        }

    }
}
