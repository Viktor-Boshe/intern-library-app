using MySql.Data.MySqlClient;
using System;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace Library_Rental
{
    public class Checkout
    {
        private const string connstring = "server=localhost;uid=root;pwd=;database=databse";
        private int user_id;
        private int book_id;
        private bool admin;

        public Checkout(int book_availability, int loggedInUserID, int bookID, bool admin)
        {
            this.user_id = loggedInUserID;
            this.book_id = bookID;
            Checkout_Check(book_availability, user_id, book_id);
            this.admin = admin;
        }

        public void Checkout_Check(int availability, int user_id, int book_id)
        {
            if (availability != 0)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you would like to rent this book?", "Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    DateTime rentedTime = DateTime.Now;
                    DateTime returnDate = rentedTime.AddMonths(3);
                    try
                    {
                        using (var conn = new MySqlConnection(connstring))
                        {
                            conn.Open();
                            string query = "INSERT INTO checkout (checkout_user_id, checkout_book_id, rented_time, return_date) VALUES (@checkout_user_id, @checkout_book_id, @rented_time, @return_date)";
                            using (MySqlCommand cmd = new MySqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@checkout_user_id", user_id);
                                cmd.Parameters.AddWithValue("@checkout_book_id", book_id);
                                cmd.Parameters.AddWithValue("@rented_time", rentedTime);
                                cmd.Parameters.AddWithValue("@return_date", returnDate);
                                cmd.ExecuteNonQuery();
                                conn.Close();
                                MessageBox.Show($"Your book has been rented please return it in due date {returnDate}");
                            }
                            string query1 = "UPDATE library SET book_availability = book_availability - 1 WHERE book_id = @book_id";
                            using (MySqlCommand cmd = new MySqlCommand(query1, conn))
                            {
                                cmd.Parameters.AddWithValue("@book_id", book_id);
                                cmd.ExecuteNonQuery();
                                conn.Close();
                                Library form = new Library(user_id, admin);
                                form.ShowDialog();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("duplicate_check"))
                        {
                            MessageBox.Show("you have already rented this book TF you DOIN?");
                        }
                        else if (ex.Message.Contains("rented_books_check"))
                        {
                            MessageBox.Show("YOU ARE TRYING TO EXCEED THE NUMBER OF AVAILABLE RENTS PER ACCOUNT GO RETURN A BOOK RIGHT NOW!!!");
                        }
                        else MessageBox.Show(ex.Message);
                        Library form = new Library(user_id, admin);
                        form.ShowDialog();
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please check for availability next time you order stop wasting my time");
            }
        }

    }
}
