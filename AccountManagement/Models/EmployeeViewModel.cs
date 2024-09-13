using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AccountManagement.Models
{
    public class EmployeeViewModel
    {
            public int EmployeeId { get; set; }
            public string Name { get; set; }
            public string Department { get; set; }
            public string JobTitle { get; set; }
            public string RemoteWorkStatus { get; set; }


        private string _encryptedSalary;

            [NotMapped]
            public decimal Salary
            {
                get => DecryptSalary(_encryptedSalary);
                set => _encryptedSalary = EncryptSalary(value);
            }

            private string EncryptSalary(decimal value)
            {

                // Use DataProtection API or other encryption
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(value.ToString()));
            }

            private decimal DecryptSalary(string encryptedValue)
            {
            if (string.IsNullOrEmpty(encryptedValue))
                return 0;

                var decrypted = Encoding.UTF8.GetString(Convert.FromBase64String(encryptedValue));
                return decimal.Parse(decrypted);
            }
        }
}
