using MySql.Data.MySqlClient;
using Dapper;
using LibraryApiService.Interface;
using LibraryApiService.Security;
namespace LibraryApiService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private string connstring;

        public UserRepository(string connstring)
        {
            this.connstring = connstring;
        }

        public void AddUser(Users user)
        {
            try
            {
                using (var conn = new MySqlConnection(connstring))
                {
                    conn.Open();
                    string query = "INSERT INTO users (username,password) VALUES (@Username,@Password)";
                    var hashedpassword = PasswordHasher.HashPassword(user.password);
                    conn.Execute(query, new { Username = user.username, Password = hashedpassword });
                }
            }
            catch
            {
                throw;
            }
        }
        public IEnumerable<Users> GetUsers()
        {
            try
            {
                using (var conn = new MySqlConnection(connstring))
                {
                    conn.Open();
                    string query = "SELECT * FROM users";
                    return conn.Query<Users>(query).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<Users> GetUser(string username)
        {
            try
            {
                using (var conn = new MySqlConnection(connstring))
                {
                    conn.Open();
                    string query = "SELECT * FROM users WHERE username = @username";
                    return conn.Query<Users>(query, new { username });
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
