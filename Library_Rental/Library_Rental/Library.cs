using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using RestSharp;
using Newtonsoft.Json;

namespace Library_Rental
{
    public partial class Library : Form
    {
        private const string connstring = "server=localhost;uid=root;pwd=;database=databse";
        private int LoggedInUserID;
        private int BookID;
        private bool admin;
        private int book_availability;
        private int book_id;
        private string book_name;

        public Library(int userID, bool admin)
        {
            InitializeComponent();
            readAvailableBooks();
            this.LoggedInUserID = userID;
            this.admin = admin;
            if (admin)
            {
                EditLibraryButton.Visible = true;
            }
        }
        private void readAvailableBooks()
        {
            using (var conn = new MySqlConnection(connstring))
            {
                conn.Open();
                string query = "SELECT * FROM library";
                try
                {

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        MySqlDataReader reader = cmd.ExecuteReader();
                        var data = new DataTable();
                        data.Load(reader);
                        conn.Close();
                        var result = (from row in data.AsEnumerable()
                                      select new
                                      {
                                          id = row.Field<int>("book_id"),
                                          book_name = row.Field<string>("book_name"),
                                          book_availability = row.Field<int>("book_availability"),
                                          book_author = row.Field<string>("book_author"),
                                          book_description = row.Field<string>("book_description")
                                      }).ToList();
                        books_list.DataSource = result;
                        books_list.DisplayMember = "book_name";
                        books_list.ValueMember = "id";

                    }
                }
                catch (Exception e)
                {
                    {
                        MessageBox.Show(e.Message);
                    }

                }
            }
        }
        private void books_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (books_list.SelectedIndex != -1)
            {
                var selectedBook = (dynamic)books_list.SelectedItem;
                BookID = (int)selectedBook.id;
                book_availability = (int)selectedBook.book_availability;

                var author = (string)selectedBook.book_author;
                var description = (string)selectedBook.book_description;
                var availability = (int)selectedBook.book_availability;

                AuthorNameBox.Text = author.ToString();
                DetailsBox.Text = "Description: " + description.ToString() + "\n\n" + "Book Availability: " + availability;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Checkout checkout = new Checkout(book_availability, LoggedInUserID, BookID, admin);
            this.Close();
        }

        private void ReturnBook_button_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connstring))
                {
                    conn.Open();
                    string query = "SELECT checkout_book_id FROM checkout WHERE checkout_user_id = @user_id";
                    using (MySqlCommand findBookCommand = new MySqlCommand(query, conn))
                    {
                        findBookCommand.Parameters.AddWithValue("@user_id", LoggedInUserID);
                        MySqlDataReader reader = findBookCommand.ExecuteReader();
                        while (reader.Read())
                        {
                            book_id = (int)reader["checkout_book_id"];
                        }
                        reader.Close();
                        query = "SELECT book_name FROM library WHERE book_id = @book_id";
                        using (MySqlCommand getBookNameCommand = new MySqlCommand(query, conn))
                        {
                            getBookNameCommand.Parameters.AddWithValue("@book_id", book_id);
                            reader = getBookNameCommand.ExecuteReader();
                            while (reader.Read())
                            {
                                book_name = (string)reader["book_name"];
                            }
                            reader.Close();
                            DialogResult dialogResult = MessageBox.Show($"are you sure you would like to return {book_name}?", "Confirmation", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                query = "UPDATE library SET book_availability = book_availability + 1 WHERE book_id = @book_id";
                                using (MySqlCommand updateBookAvailability = new MySqlCommand(query, conn))
                                {
                                    updateBookAvailability.Parameters.AddWithValue("@book_id", book_id);
                                    updateBookAvailability.ExecuteNonQuery();
                                }
                                query = "DELETE FROM checkout WHERE checkout_user_id = @user_id AND checkout_book_id = @book_id";
                                using (MySqlCommand deleteCheckoutCommand = new MySqlCommand(query, conn))
                                {
                                    deleteCheckoutCommand.Parameters.AddWithValue("@user_id", LoggedInUserID);
                                    deleteCheckoutCommand.Parameters.AddWithValue("@book_id", book_id);
                                    deleteCheckoutCommand.ExecuteNonQuery();
                                    readAvailableBooks();
                                }

                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Search_button_Click(object sender, EventArgs e)
        {
            searchResults();
        }
        private void searchResults()
        {
            using (var conn = new MySqlConnection(connstring))
            {
                conn.Open();
                string query = "SELECT * FROM library WHERE LOWER(book_name) LIKE LOWER(@search_title) AND LOWER(book_author) LIKE LOWER(@search_author)";
                try
                {

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@search_title", '%'+searchBox_title.Text+'%');
                        cmd.Parameters.AddWithValue("@search_author", '%'+textBox2.Text+'%');
                        MySqlDataReader reader = cmd.ExecuteReader();
                        var data = new DataTable();
                        data.Load(reader);
                        conn.Close();
                        var result = (from row in data.AsEnumerable()
                                      select new
                                      {
                                          id = row.Field<int>("book_id"),
                                          book_name = row.Field<string>("book_name"),
                                          book_availability = row.Field<int>("book_availability"),
                                          book_author = row.Field<string>("book_author"),
                                          book_description = row.Field<string>("book_description")
                                      }).ToList();
                        books_list.DataSource = result;
                        books_list.DisplayMember = "book_name";
                        books_list.ValueMember = "id";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
