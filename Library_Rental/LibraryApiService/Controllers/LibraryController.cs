using LibraryApiService.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryRepository _libraryRepository;

        public LibraryController(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Library>> GetBooks()
        {
            var books = _libraryRepository.GetBooks();
            return Ok(books);
        }
        [HttpPost("GetBooksByIds")]
        public ActionResult<IEnumerable<Library>> GetBooksByIds([FromBody] List<int> bookIds)
        {
            var books = _libraryRepository.GetBooksByIds(bookIds);
            return Ok(books);
        }
    }
}
