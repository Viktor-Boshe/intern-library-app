namespace LibraryApiService.Interface
{
    public interface ILibraryRepository
    {
        IEnumerable<Library> GetBooks();
        IEnumerable<Library> GetBooksByIds(List<int> bookIds);
    }
}
