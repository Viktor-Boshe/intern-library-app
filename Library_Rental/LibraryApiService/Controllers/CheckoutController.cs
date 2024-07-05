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
        public ActionResult Checkout(string user, int book_id)
        {
            try
            {
                var jwtHandler = new JwtSecurityTokenHandler();
                var jwtToken = jwtHandler.ReadToken(user) as JwtSecurityToken;

                if (jwtToken == null)
                {
                    throw new ArgumentException("Invalid JWT token");
                }

                var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "user_id");
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
        public ActionResult Delete(string user,int book_id)
        {
            try
            {
                var jwtHandler = new JwtSecurityTokenHandler();
                var jwtToken = jwtHandler.ReadToken(user) as JwtSecurityToken;

                if (jwtToken == null)
                {
                    throw new ArgumentException("Invalid JWT token");
                }

                var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "user_id");
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
        public ActionResult Get(string user)
        {
            try
            {
                var jwtHandler = new JwtSecurityTokenHandler();
                var jwtToken = jwtHandler.ReadToken(user) as JwtSecurityToken;

                if (jwtToken == null)
                {
                    throw new ArgumentException("Invalid JWT token");
                }

                var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "user_id");

                var books = _checkoutRepository.getBooks(int.Parse(userIdClaim.Value));
                return Ok(books);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
