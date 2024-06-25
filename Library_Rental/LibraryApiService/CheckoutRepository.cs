﻿using Dapper;
using MySql.Data.MySqlClient;
using System.Diagnostics.Eventing.Reader;

namespace LibraryApiService
{
    public class CheckoutRepository : ICheckoutRepository
    {
        private string connstring = "server=localhost;uid=root;pwd=;database=databse";

        public void AddCheckout(Checkout checkout)
        {
            try
            {
                using (var conn = new MySqlConnection(connstring))
                {
                    conn.Open();
                    DateTime rented_time = DateTime.Now;
                    DateTime return_date = rented_time.AddMonths(3);
                    string query = "INSERT INTO checkout (checkout_user_id,checkout_book_id,rented_time,return_date) VALUES (@user_id,@book_id,@rented_time,@return_date)";
                    conn.Execute(query, new { user_id = checkout.checkout_user_id, book_id = checkout.checkout_book_id, rented_time, return_date });
                    conn.Close();
                }
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteCheckout(Checkout checkout)
        {
            try
            {
                using (var conn = new MySqlConnection(connstring))
                {
                    conn.Open();
                    string query = "DELETE FROM checkout WHERE checkout_user_id = @user_id AND checkout_book_id = @book_id";
                    conn.Execute(query, new { user_id = checkout.checkout_user_id, book_id = checkout.checkout_book_id });
                    conn.Close();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Library> getBooks(int user_id)
        {
            List<int> bookIds = new List<int>();
            List<Library> books = new List<Library>();
            try
            {
                using (var conn = new MySqlConnection(connstring))
                {
                    conn.Open();
                    string query = "SELECT checkout_book_id FROM checkout WHERE checkout_user_id = @user_id";
                    bookIds = conn.Query<int>(query,new { user_id }).ToList();
                }
                using (var conn =new MySqlConnection(connstring))
                {
                    conn.Open();
                    foreach (int book_id in bookIds)
                    {
                        string query = "SELECT * FROM Library where book_id = @book_id";
                        var book = conn.QueryFirstOrDefault<Library>(query, new { book_id });
                        if(book != null)
                        {
                            books.Add(book);
                        }
                    }
                    return books;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
