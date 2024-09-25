using EmployeeManagementAPI.Core.Interface;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagementAPI.Core.Entities;
using Microsoft.AspNetCore.Identity.Data;

namespace EmployeeManagementAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUser _userRepository;
        private readonly IAuth _authRepository;

        public UserController(IUser userRepository, IAuth authRepository)
        {
            _userRepository = userRepository;
            _authRepository = authRepository;
        }

        [HttpPost]
        [Route("login/")]
        public async Task<ActionResult<User>> Login(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            await _userRepository.LogInAsync(user);

            return Ok();
        }

        [HttpPost]
        [Route("logout/")]
        public async Task<ActionResult> Logout()
        {
            var user = await _userRepository.GetCurrentUserAsync();
            if (user == null)
            {
                return BadRequest("Nobody is logged in..");
            }
            await _userRepository.LogOutAsync(user);
            return Ok();
        }

        [HttpPost]
        [Route("clear/")]
        public async Task<ActionResult> ClearUser()
        {
            await _userRepository.ClearUserAsync();
            return Ok();
        }

        [HttpGet]
        [Route("CurrentString/")]
        public async Task<ActionResult<string>> GetCurrentUserString()
        {
            var user = await _userRepository.GetCurrentUserStringAsync();
            if(user != null)
            {
                return Ok(user);
            }
            return Ok("");
        }

        [HttpGet]
        [Route("Current/")]
        public async Task<ActionResult<User>> GetCurrentUser()
        {
            var user = await _userRepository.GetCurrentUserAsync();
            if( user != null )
            {
                return Ok(user);
            }
            return new User { Name = "Null", Email = null };
        }
    }
}
