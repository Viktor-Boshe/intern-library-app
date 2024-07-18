namespace LibraryApiService
{
    public class EmailSender
    {
        public int UserId {  get; set; }
        public int BookId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
