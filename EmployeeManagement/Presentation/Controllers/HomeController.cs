using EmployeeManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using EmployeeManagement.Domain.Interface;
using EmployeeManagement.Application.Interface;

namespace EmployeeManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IAuth _authService;
        private readonly IUser _userService;

        public HomeController(ILogger<HomeController> logger, IUserRepository userRepository,IAuth authService, IUser userService)
        {
            _logger = logger;
            _userRepository = userRepository;
            _authService = authService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            await _userService.ClearUserAsync();
            return View("Index");
        }

        public IActionResult Privacy()
        {
            return View("Privacy");
        }

        public async Task<IActionResult> Home()
        {
            return View("Home");
        }

        public async Task<IActionResult> Profile()
        {
            var user = await _userService.GetCurrentUserAsync();
            var details = await _authService.GetUserDetailsAsync(user);
            ViewBag.CurrentUser = details.Name;
            ViewBag.UserEmail = details.Email;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
