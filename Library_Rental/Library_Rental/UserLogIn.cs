using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Library_Rental
{
    public partial class UserLogIn : Form
    {
        private const string connstring = "server=localhost;uid=root;pwd=;database=databse";
        private int userID;
        private bool admin;
        public UserLogIn()
        {
            InitializeComponent();
        }

        private bool ValidateUser(string username, string password)
        {
            bool flag = true;
            using (var conn = new MySqlConnection(connstring))
            {
                conn.Open();
                string query = "SELECT * FROM users WHERE username = @username";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    var result = string.Empty;
                    while (reader.Read())
                    {
                        result = (string)reader["password"];
                        userID = Convert.ToInt32(reader["user_id"]);
                        admin = (bool)reader["administrator"];
                    }
                    if (PasswordHasher.checkPassword(password, result.ToString()))
                    {
                        this.Hide();
                        Library form = new Library(userID,admin);
                        form.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("username or password incorrect please enter the correct data or create a new account");
                        password_textbox.Clear();
                        username_textbox.Clear();
                       flag = false;
                    }
                }
                
            }
            return flag;
        }

        private void LogIn_Button_Click(object sender, EventArgs e)
        {
            try
            {
                string username = username_textbox.Text;
                string password = password_textbox.Text;
                ValidateUser(username, password);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UserLogIn_Load(object sender, EventArgs e)
        {

        }
    }
}
