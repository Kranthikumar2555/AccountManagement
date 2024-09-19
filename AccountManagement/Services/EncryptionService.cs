using Microsoft.AspNetCore.DataProtection;

namespace CourseManagement.Services
{
    public class EncryptionService
    {
        private readonly IDataProtector _protector;

        public EncryptionService(IDataProtectionProvider dataProtectionProvider)
        {
            // Create a protector with a purpose string
            _protector = dataProtectionProvider.CreateProtector("EmailProtection");
        }

        // Encrypt Email
        public string EncryptEmail(string email)
        {
            return _protector.Protect(email);
        }

        // Decrypt Email
        public string DecryptEmail(string encryptedEmail)
        {
            return _protector.Unprotect(encryptedEmail);
        }
    }

}
