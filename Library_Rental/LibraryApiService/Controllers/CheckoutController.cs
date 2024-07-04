using LibraryApiService.Interface;
using Microsoft.AspNetCore.Mvc;
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
        public ActionResult Checkout(Checkout checkout)
        {
            try
            {
                _checkoutRepository.AddCheckout(checkout);
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
        [HttpDelete("removeBook")]
        public ActionResult Delete(int user_id,int book_id)
        {
            try
            {
                var checkout = new Checkout
                {
                    checkout_user_id = user_id,
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
        public ActionResult Get(int id)
        {
            try
            {
                var books = _checkoutRepository.getBooks(id);
                return Ok(books);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
