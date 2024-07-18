namespace LibraryApiService.Interface
{
    public interface ILibraryRepository
    {
        IEnumerable<Library> GetBooks(bool show);
        IEnumerable<Library> GetBooksByIds(List<int> bookIds);
    }
}
