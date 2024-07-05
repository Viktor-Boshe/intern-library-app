using LibraryApiService.Interface;
using LibraryApiService.Security;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenGenerator _tokenGenerator;

        public UsersController(IUserRepository userRepository, TokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;

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
                var token = _tokenGenerator.GenerateToken(user);
                return Ok(new { success = true, token });
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