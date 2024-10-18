using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Domain.Interface;
using EmployeeManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using EmployeeManagement.Application.Interface;

namespace EmployeeManagement.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAuth _authService;

        public AdminController(IAuth authService)
        {
            _authService = authService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            ViewBag.PageTitle = "Registration";
            ViewBag.CardHeader = "Register a New User";
            ViewBag.Department = "Registration";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Id,Name,Email,Password")] Auth newUser)
        {
            if (ModelState.IsValid)
            {
                await _authService.RegisterAsync(newUser);
                TempData["register"] = true;
                return RedirectToAction(nameof(ViewUsers));
            }
            return View("Error");
        }

        public async Task<IActionResult> ViewUsers()
        {
            var users = await _authService.GetAllUsersAsync();
            ViewBag.PageTitle = "All Users";
            ViewBag.CardHeader = "View All Users";
            ViewBag.Department = "Users";
            return View(users);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var user = await _authService.GetUserById(id);
            if(user == null)
            {
                return NotFound();
            }
            ViewBag.PageTitle = $"Edit {user.Name}";
            ViewBag.CardHeader = "Edit";
            ViewBag.Department = "Edit";
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Email, Password")] Auth user)
        {
            if(id != user.Id)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                try
                {
                    await _authService.UpdateUserAsync(user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound("User does not exist");
                }
                return RedirectToAction(nameof(ViewUsers));
            }
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _authService.DeleteUserAsync(id);

            TempData["delete"] = true;

            return RedirectToAction(nameof(ViewUsers));
        }
    }
}
