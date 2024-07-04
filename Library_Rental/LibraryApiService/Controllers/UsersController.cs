using LibraryApiService.Interface;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpPost("create")]
        public ActionResult AddUser(Users user)
        {
            try
            {
                _userRepository.AddUser(user);
                return Ok(new { success = true }); ;
            }
            catch
            {
                return Unauthorized(new { success = false, message = "Username is already in use" }); ;
            }
        }
        [HttpPost("login")]
        public ActionResult Login([FromBody] Users loginDetails)
        {
            var user = _userRepository.GetUsers().FirstOrDefault(u => u.username == loginDetails.username);

            if (user != null && PasswordHasher.checkPassword(loginDetails.password, user.password))
            {
                return Ok(new { success = true, user });
            }
            else
            {
                return Unauthorized(new { success = false, message = "Invalid username or password." });
            }
        }
        [HttpGet]
        public ActionResult<IEnumerable<Users>> GetUsers()
        {
            var users = _userRepository.GetUsers();
            return Ok(users);
        }
    }
}