using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace Library_Rental
{
    public partial class UserCreation : Form
    {
        private const string connstring = "server=localhost;uid=root;pwd=;database=databse";

        public UserCreation()
        {
            InitializeComponent();
        }
        private bool isUsernameinUse(string username)
        {
            using (var conn = new MySqlConnection(connstring))
            {
                conn.Open();
                string query = "SELECT * FROM users WHERE username = @username";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    conn.Close();
                    return count > 0;
                }
            }
        }
        private bool CheckPassword(string password)
        {
            if (password == null || password.Length > 18 || password.Length < 5)
            {
                return false;
            }
            return true;
        }
        private void insert(string username, string password)
        {
            if (isUsernameinUse(username))
            {
                MessageBox.Show("Username is already in use. Please choose a different username.");
                username_textbox.Clear();
                return;
            }
            if (!CheckPassword(password))
            {
                MessageBox.Show("Please enter password minimum 5 maximum 18 characters");
                password_textbox.Clear();
                return;
            }
            var hashedpassword = PasswordHasher.HashPassword(password);
            using (var conn = new MySqlConnection(connstring))
            {
                conn.Open();
                string query = "INSERT INTO users (username,password)" + "VALUES (@username,@password)";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", hashedpassword);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

            }
            MessageBox.Show("User Created Successfully!");
            this.Close();
        }

        private void Create_btn_Click(object sender, EventArgs e)
        {
            try
            {
                string username = username_textbox.Text;
                string password = password_textbox.Text;

                insert(username, password);
            }
            catch (Exception ex) 
            { 
                MessageBox.Show($"Error Creating USer: {ex.Message}");
            }
        }
    }
}
