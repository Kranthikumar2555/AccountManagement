namespace CourseManagement.Services
{
    public interface IEncryptionService
    {
        string? EncryptEmail(string? email);
        string? DecryptEmail(string? encryptedEmail);
    }
}
