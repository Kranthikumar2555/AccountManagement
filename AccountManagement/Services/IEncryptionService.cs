namespace CourseManagement.Services
{
    /// <summary>
    /// Provides methods for encrypting and decrypting sensitive information, such as email addresses.
    /// </summary>
    public interface IEncryptionService
    {
        /// <summary>
        /// Encrypts the provided email address.
        /// </summary>
        /// <param name="email">The email address to be encrypted. It can be null.</param>
        /// <returns>The encrypted email as a string, or null if the input is null.</returns>
        string? EncryptEmail(string? email);

        /// <summary>
        /// Decrypts the provided encrypted email.
        /// </summary>
        /// <param name="encryptedEmail">The encrypted email to be decrypted. It can be null.</param>
        /// <returns>The decrypted email as a string, or null if the input is null.</returns>
        string? DecryptEmail(string? encryptedEmail);
    }
}
