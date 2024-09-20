using Microsoft.AspNetCore.DataProtection;

namespace CourseManagement.Services
{
    /// <summary>
    /// Provides encryption and decryption services for sensitive data such as email addresses.
    /// </summary>
    public class EncryptionService : IEncryptionService
    {
        private readonly IDataProtector _protector;

        /// <summary>
        /// Initializes a new instance of the <see cref="EncryptionService"/> class with the specified data protection provider.
        /// </summary>
        /// <param name="dataProtectionProvider">The data protection provider used to create a protector for encryption and decryption.</param>
        public EncryptionService(IDataProtectionProvider dataProtectionProvider)
        {
            // Create a protector for email encryption using a specific purpose string for security
            _protector = dataProtectionProvider.CreateProtector("EmailProtection");
        }

        /// <summary>
        /// Encrypts the provided email address using data protection.
        /// </summary>
        /// <param name="email">The email address to be encrypted. Can be null.</param>
        /// <returns>The encrypted email as a string, or null if the input is null or empty.</returns>
        public string? EncryptEmail(string? email)
        {
            // Return null if the email is null or empty
            if (string.IsNullOrEmpty(email))
            {
                return null;
            }

            // Encrypt and return the email address
            return _protector.Protect(email);
        }

        /// <summary>
        /// Decrypts the provided encrypted email address using data protection.
        /// </summary>
        /// <param name="encryptedEmail">The encrypted email address to be decrypted. Can be null.</param>
        /// <returns>The decrypted email as a string, or null if the input is null or empty.</returns>
        public string? DecryptEmail(string? encryptedEmail)
        {
            // Return null if the encrypted email is null or empty
            if (string.IsNullOrEmpty(encryptedEmail))
            {
                return null;
            }

            // Decrypt and return the email address
            return _protector.Unprotect(encryptedEmail);
        }
    }
}
