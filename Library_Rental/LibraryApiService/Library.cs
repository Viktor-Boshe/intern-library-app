using System.Reflection.Metadata;

namespace LibraryApiService
{
    public class Library
    {
        public int book_id { get; set; }
        public string book_name { get; set; }
        public int book_availability { get; set; }
        public string book_author { get; set; }
        public string book_description { get; set; }
        public byte[] book_coverImg { get; set; }
    }
}
