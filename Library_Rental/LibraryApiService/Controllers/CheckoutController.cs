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
                throw new Exception(ex.Message);
            }
        }
        [HttpDelete]
        public ActionResult Delete(Checkout checkout)
        {
            try
            {
                _checkoutRepository.DeleteCheckout(checkout);
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet]
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
