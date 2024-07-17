using Dapper;
using LibraryApiService.Interface;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace LibraryApiService.Repositories
{
    public class LibraryRepository : ILibraryRepository
    {
        private string connstring;

        public LibraryRepository(string connstring)
        {
            this.connstring = connstring;
        }

        public IEnumerable<Library> GetBooks(bool show)
        {
            try
            {
                if (show)
                {
                    List<int> ids = new List<int>();
                    List<Library> books = new List<Library>();
                    Random random = new Random();
                    int count = 50;
                    while (count > 0)
                    {
                        ids.Add(random.Next(19332, 66121));
                        count--;
                    }
                    using (MySqlConnection conn = new MySqlConnection(connstring))
                    {
                        conn.Open();
                        foreach (int id in ids)
                        {
                            string query = "SELECT * FROM library WHERE book_id = @book_id";
                            books.Add(item: conn.Query<Library>(query, new { book_id = id }).SingleOrDefault());
                        }
                        return books;
                    }
                }
                else
                {
                    using (MySqlConnection conn = new MySqlConnection(connstring))
                    {
                        conn.Open();
                        string query = "SELECT * FROM library WHERE URL IS NOT NULL";
                        var books = conn.Query<Library>(query, new { });
                        return books;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<Library> GetBooksByIds(List<int> bookIds)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connstring))
                {
                    conn.Open();
                    var books = conn.Query<Library>("SELECT * FROM library WHERE book_id IN @bookIds", new { bookIds });
                    return books;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
