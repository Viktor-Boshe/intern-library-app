using LibraryApiService.Interface;
using LibraryApiService.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailSender _emailSender;

        public EmailController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpPost("send")]
        [Authorize]
        public async Task<IActionResult> SendEmail(int book_id)
        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "user_id");

                if (userIdClaim == null)
                {
                    throw new ArgumentException("Missing user_id claim");
                }

                var emailSender = new EmailSender
                {
                    UserId = int.Parse(userIdClaim.Value),
                    BookId = book_id,
                    Subject = "Your book has been rented",
                };
                await _emailSender.SendEmailAsync(emailSender);
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
