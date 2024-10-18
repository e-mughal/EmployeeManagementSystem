using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Domain.Interface;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Application.Interface;

namespace EmployeeManagement.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuth _authService;
        private readonly IUser _userService;

        public AuthController(IUserRepository userRepository, IAuth authService, IUser userService)
        {
            _userRepository = userRepository;
            _authService = authService;
            _userService = userService; 
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Login(string email, string password)
        {
            if(ModelState.IsValid)
            {
                if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    return View("Error");
                }

                var success = await _authService.LoginAsync(email, password);
                if(success)
                {
                    var name = await _authService.GetNameAsync(email, password);

                    var user = new User
                        { Name = name.Name, Email = email };

                    await _userService.LogInAsync(user);

                    return RedirectToAction("Success");
                }
                else
                {
                    TempData["LoginFail"] = true;
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([Bind("Id,Name,Email, Password")] Auth newUser)
        {
            if (ModelState.IsValid)
            {
                await _authService.RegisterAsync(newUser);
                return RedirectToAction("ViewUsers", "Admin");
            }

            return RedirectToAction("Fail");
        }

        public async Task<IActionResult> Logout()
        {
            var user = await _userService.GetCurrentUserAsync();
            await _userService.LogOutAsync(user);
            return RedirectToAction("Index", "Home");
        }
        
        public IActionResult Success()
        {
            return View();
        }
    }
}