using LibraryApiService.Interface;
using System.Net.Mail;
using System.Net;
using MySql.Data.MySqlClient;
using Dapper;

namespace LibraryApiService.Repositories
{
    public class EmailSenderRepository : IEmailSender
    {
        private readonly string _email;
        private readonly string _password;
        private readonly string _connstring;

        public EmailSenderRepository(string email, string password, string connstring)
        {
            _email = email;
            _password = password;
            _connstring = connstring;
        }
        public async Task SendEmailAsync(EmailSender emailSender)
        {
            string recipientEmail;
            try
            {
                var smtpClient = new SmtpClient("smtp-mail.outlook.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(_email, _password),
                    EnableSsl = true,
                };
                using (var conn = new MySqlConnection(_connstring))
                {
                    conn.Open();
                    string emailQuery = "SELECT e_mail FROM users WHERE user_id = @user_id";
                    recipientEmail = conn.QuerySingleOrDefault<string>(emailQuery, new { user_id = emailSender.UserId });
                    if(recipientEmail == null)
                    {
                        throw new ArgumentException("YOU HAVE NO MAIL");
                    }
                    string urlQuery = "SELECT URL FROM library WHERE book_id = @book_id";
                    emailSender.Body = conn.QuerySingleOrDefault<string>(urlQuery, new { book_id = emailSender.BookId });
                    if (emailSender.Body == null)
                    {
                        throw new ArgumentException("BOOK IS NOT AVAILABLE FOR DIGITAL PURCHASE YOU CAN ONLY PICK IT UP TURN OFF SHOW ALL BOOKS IF YOU WANT TO SEE WHAT YOU CAN GET");
                    }
                    conn.Close();
                }
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_email, "Book Library"),
                    Subject = emailSender.Subject,
                    Body = emailSender.Body,
                    IsBodyHtml = true,
                };

                mailMessage.To.Add(recipientEmail);

                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
