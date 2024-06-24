namespace LibraryApiService
{
    public class Checkout
    {
        public int checkout_user_id {  get; set; }
        public int checkout_book_id { get; set; }
        public DateTime rented_time { get; set; }
        public DateTime return_date { get; set; }
    }
}
