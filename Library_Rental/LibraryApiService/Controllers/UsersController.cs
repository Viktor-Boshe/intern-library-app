using LibraryApiService.Interface;
using LibraryApiService.Security;
using Microsoft.AspNetCore.Authorization;
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
        [HttpPut("reset")]
        public ActionResult ResetPassword(Users user) 
        {
            try
            {
                _userRepository.ResetPassword(user);
                return Ok(new { success = true });
            }
            catch
            {
                return Unauthorized(new { success = false, message = "Unable to change password" });
            }
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] Users loginDetails)
        {
            var user = _userRepository.GetUser(loginDetails.username).FirstOrDefault(u => u.username == loginDetails.username);

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
        [HttpPost("refresh")]
        [Authorize]
        public ActionResult RefreshToken()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var validation = _tokenGenerator.ValidateToken(token);
            if (validation == null)
            {
                return Unauthorized(new { success = false, message = "Invalid token" });
            }

            var newToken = _tokenGenerator.RefreshToken(validation);
            return Ok(new { success = true, token = newToken });
        }
        [HttpGet("validate")]
        [Authorize]
        public ActionResult TokenValidation()
        {
            var authHeader = HttpContext.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authHeader))
            {
                return Unauthorized(new { message = "No authorization header found." });
            }
            var token = authHeader.ToString().Replace("Bearer ", "");
            return Ok(new { success = true, token });
        }
        [HttpGet("user")]
        public ActionResult<IEnumerable<Users>> GetUser(string username)
        {
            var user = _userRepository.GetUser(username);
            return Ok(new { success = true, user });
        }
    }
}