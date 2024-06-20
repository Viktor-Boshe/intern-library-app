using System;
using System.Windows.Forms;

namespace Library_Rental
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Create_User_btn_Click(object sender, EventArgs e)
        {
            UserCreation form = new UserCreation();
            form.ShowDialog();
        }

        private void login_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserLogIn form = new UserLogIn();
            form.ShowDialog();
            this.Close();
        }
    }
}
