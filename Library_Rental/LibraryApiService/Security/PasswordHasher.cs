using System.Security.Cryptography;
using System.Text;

namespace LibraryApiService.Security
{
    internal class PasswordHasher
    {
        private const int size = 16;
        private const int iterations = 10000;
        public static string HashPassword(string password)
        {
            byte[] bytes = Generate();
            byte[] hashedBytes = HashIt(Encoding.UTF8.GetBytes(password), bytes, iterations);

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(Convert.ToBase64String(bytes));
            stringBuilder.Append(":");
            stringBuilder.Append(Convert.ToBase64String(hashedBytes));

            return stringBuilder.ToString();
        }

        public static bool checkPassword(string password, string hashedPassword)
        {
            string[] parts = hashedPassword.Split(':');
            if (parts.Length != 2)
            {
                return false;
            }
            byte[] bytes = Convert.FromBase64String(parts[0]);
            byte[] hashedBytes = HashIt(Encoding.UTF8.GetBytes(password), bytes, iterations);

            return parts[1] == Convert.ToBase64String(hashedBytes);
        }

        public static byte[] Generate()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] bytes = new byte[size];
                rng.GetBytes(bytes);
                return bytes;
            }
        }
        public static byte[] HashIt(byte[] inputBytes, byte[] bytes, int iterations)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(inputBytes, bytes, iterations))
            {
                return pbkdf2.GetBytes(32);
            }
        }
    }
}
