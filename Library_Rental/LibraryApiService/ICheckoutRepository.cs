namespace LibraryApiService
{
    public interface ICheckoutRepository
    {
        void AddCheckout(Checkout checkout);

        void DeleteCheckout(Checkout checkout);
        IEnumerable<Library> getBooks(int user_id);
    }
}
