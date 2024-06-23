using MySql.Data.MySqlClient;
using Dapper;
namespace LibraryApiService
{
    public class UserRepository : IUserRepository
    {
        private string connstring = "server=localhost;uid=root;pwd=993388;database=databse";
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
    }
}
