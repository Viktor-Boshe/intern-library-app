namespace LibraryApiService
{
    public interface ILibraryRepository
    {
        IEnumerable<Library> GetBooks();
    }
}
