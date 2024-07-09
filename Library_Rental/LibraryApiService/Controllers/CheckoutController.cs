using LibraryApiService.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1;
using System.IdentityModel.Tokens.Jwt;
using ZstdSharp.Unsafe;

namespace LibraryApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckoutController : ControllerBase
    {
        private readonly ICheckoutRepository _checkoutRepository;

        public CheckoutController(ICheckoutRepository checkoutRepository)
        {
            _checkoutRepository = checkoutRepository;
        }
        [HttpPost]
        [Authorize]
        public ActionResult Checkout(int book_id)
        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "user_id");

                if (userIdClaim == null)
                {
                    throw new ArgumentException("Missing user_id claim");
                }

                var checkout = new Checkout
                {
                    checkout_user_id = int.Parse(userIdClaim.Value),
                    checkout_book_id = book_id
                };

                _checkoutRepository.AddCheckout(checkout);
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
        [HttpDelete("removeBook")]
        [Authorize]
        public ActionResult Delete(int book_id)
        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "user_id");


                if (userIdClaim == null)
                {
                    throw new ArgumentException("Invalid JWT token");
                }
                var checkout = new Checkout
                {
                    checkout_user_id = int.Parse(userIdClaim.Value),
                    checkout_book_id = book_id
                };
                _checkoutRepository.DeleteCheckout(checkout);
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("getbooks")]
        [Authorize]
        public ActionResult Get()
        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "user_id");

                if (userIdClaim == null)
                {
                    throw new ArgumentException("Missing user_id claim");
                }

                var books = _checkoutRepository.getBooks(int.Parse(userIdClaim.Value));
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
