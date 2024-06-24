using Dapper;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace LibraryApiService
{
    public class LibraryRepository : ILibraryRepository
    {
        public IEnumerable<Library> GetBooks()
        {
            string connstring = "server=localhost;uid=root;pwd=;database=databse";
            try
            {
                using(MySqlConnection conn = new MySqlConnection(connstring))
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
    }
}
