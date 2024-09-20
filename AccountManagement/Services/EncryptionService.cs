using Microsoft.AspNetCore.DataProtection;

namespace CourseManagement.Services
{
    public class EncryptionService: IEncryptionService
    {
        private readonly IDataProtector _protector;

        public EncryptionService(IDataProtectionProvider dataProtectionProvider)
        {
            // Create a protector with a purpose string
            _protector = dataProtectionProvider.CreateProtector("EmailProtection");
        }

        // Encrypt Email
        public string? EncryptEmail(string? email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return null;  // Return null if the email is null or empty
            }
            return _protector.Protect(email);
        }

        // Decrypt Email
        public string? DecryptEmail(string? encryptedEmail)
        {
            if (string.IsNullOrEmpty(encryptedEmail))
            {
                return null;  // Return null if the email is null or empty
            }
            return _protector.Unprotect(encryptedEmail);
        }
    }

}
