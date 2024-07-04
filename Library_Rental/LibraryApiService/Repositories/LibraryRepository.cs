using Dapper;
using LibraryApiService.Interface;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace LibraryApiService.Repositories
{
    public class LibraryRepository : ILibraryRepository
    {
        private string connstring = "server=localhost;uid=root;pwd=;database=databse";
        public IEnumerable<Library> GetBooks()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connstring))
                {
                    conn.Open();
                    string query = "SELECT * FROM library";
                    return conn.Query<Library>(query).ToList();
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
