using System.Security.Cryptography;
using System.Text;

namespace AccountManagement.Helpers
{
    public static class PasswordSecurityHelper
    {

        public static bool VerifyPassword(string password, string storedHash)
        {
            // Hash the incoming password
            string hashedPassword = HashPassword(password);
            // Compare hashes
            return hashedPassword == storedHash;
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashBytes);
            }
        }


    }
}
