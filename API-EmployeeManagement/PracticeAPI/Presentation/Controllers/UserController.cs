using EmployeeManagementAPI.Core.Interface;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagementAPI.Core.Entities;
using Microsoft.AspNetCore.Identity.Data;

namespace EmployeeManagementAPI.Presentation.Controllers
{
    // API Controller class for Users
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        // creating variables to access user and auth repo functions, ensuring they remain constant
        private readonly IUser _userRepository;
        private readonly IAuth _authRepository;

        /*
         * Constructor
         * param: user repo var
         * param: auth repo var
         * 
         * Initializes the above declared variables
         */
        public UserController(IUser userRepository, IAuth authRepository)
        {
            _userRepository = userRepository;
            _authRepository = authRepository;
        }

        /*
         * Logs in a user into the system
         * param: User entity
         * 
         * Calls the user repo function, and logs in the user
         */
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

        /*
         * Logs a user out of the system
         * param: none, since only one user can be logged at a time
         * 
         * checks to see if there is a current user, logging them out accordingly.
         */
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

        /*
         * Clears the users from the current user table in the database
         */
        [HttpPost]
        [Route("clear/")]
        public async Task<ActionResult> ClearUser()
        {
            await _userRepository.ClearUserAsync();
            return Ok();
        }

        /*
         * Gets the current user a string, for front-end purposes
         */
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

        /*
         * Gets the current user as a User entity
         */
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
