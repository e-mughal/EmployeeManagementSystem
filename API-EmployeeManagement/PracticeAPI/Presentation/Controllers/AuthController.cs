using EmployeeManagementAPI.Core.Interface;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagementAPI.Core.Entities;
using PracticeAPI.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.Data;

namespace EmployeeManagementAPI.Presentation.Controllers
{
    // Creating an API controller class
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        // Creating a variable to get data from the auth repo, ensuring it cannot be altered after initialization.
        private readonly IAuth _authRepository;

        /*
         * Constructor
         * param: auth repository
         * assigns the value to our data member
         * 
         */
        public AuthController(IAuth authRepository)
        {
            _authRepository = authRepository;
        }

        /*
         * Gets the user based on the ID.
         * param: integer id
         * 
         * awaits for the auth repo's response, returns the auth with an OK message if the user exists.
         * otherwise returns a badrequest.
         */
        [HttpGet]
        [Route("id/{id}")]
        public async Task<ActionResult<Auth>> GetUserById(int id)
        {
            var auth = await _authRepository.GetUserById(id);
            if(auth == null)
            {
                return BadRequest("No user has the Id provided!");
            }
            return Ok(auth);
        }


        /*
         * Registers a new user.
         * param: takes string params and binds them to create an auth model/object.
         * 
         * checks to see if the auth model was created successfuly, and returns accordingly
         */
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Auth>> Register([Bind("Id, Name, Email, Password")] Auth auth)
        {
            if(auth == null)
            {
                return BadRequest("Error!! New User is null...");
            }
            await _authRepository.RegisterAsync(auth);
            return Ok($"Successfully Registered {auth.Name}");
        }

        /*
         * Logs in a user.
         * param: takes the users log in request from the body, i.e. the email and password entered by the user.
         * 
         * checks from the auth repo whether or not the log in successful, and returns accordingly.
         */
        [HttpPost]
        [Route("Login/")]
        public async Task<ActionResult<bool>> LogIn([FromBody] LoginRequest request)
        {
            var logIn = await _authRepository.LoginAsync(request);
            if (logIn)
            {
                return Ok(true);
            }
            return Ok(false);
        }

        /*
         * Gets the name of the user based on the provided email.
         * param: takes the user's log in request from the body, i.e. the email and password from the user.
         * 
         * checks to see if the email and password exists, and then returns the name of that user.
         * function returns accordingly.
         */
        [HttpPost]
        [Route("GetName/")]
        public async Task<ActionResult<string>> GetName([FromBody] LoginRequest request)
        {
            var name = await _authRepository.GetNameAsync(request);
            if(name == null)
            {
                return BadRequest("Name is Null");
            }
            return Ok(name);
        }

        /*
         * Gets the details of the user
         * param: user model/object
         * 
         * Get the details of the users from the auth repo if the user exists.
         * It returns accordingly.
         */
        [HttpPost]
        [Route("GetDetails/")]
        public async Task<ActionResult<Auth>> GetUserDetails(User user)
        {
            var auth = await _authRepository.GetUserDetailsAsync(user);
            if (auth == null)
            {
                return BadRequest("User is Null");
            }
            return Ok(auth);
        }

        /*
         * Gets a list of all the users
         * 
         * Uses the auth repo to get all the users and returns the result.
         */
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Auth>>> GetAllUsers()
        {
            var users = await _authRepository.GetAllUsersAsync();
            return Ok(users);
        }

        /*
         * Updates the User in the database
         * param: user id
         * param: Auth object
         * 
         * Takes in the user id and details to update. If the user exists, the user is updated.
         * Otherwise an error is thrown.
         */
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<Auth>> UpdateUser(int id, [Bind("Id, Name, Email, Password")] Auth user)
        {
            if(id != user.Id)
            {
                return BadRequest("Error! Id Missmatch!");
            }
            try
            {
                await _authRepository.UpdateUserAsync(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest("User not found!");
            }

            return NoContent();
        }

        /*
         * Deletes the user from the database.
         * param: user id
         * 
         * Delete's user by id, if user exists.
         */
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Auth>> DeleteUser(int id)
        {
            var auth = await _authRepository.GetUserById(id);
            if(auth == null)
            {
                return BadRequest("ERROR! This User does not exist in the database.");
            }
            await _authRepository.DeleteUserAsync(id);

            return NoContent();
        }
    }
}
