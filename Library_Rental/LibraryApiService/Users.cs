namespace LibraryApiService
{
    public class Users
    {
        public int user_id { get; set; }
        public string username { get; set; }
        public string password { get;  set; }
        public bool administrator { get; set; }
        public int rented_books { get; set; }
    }
}
