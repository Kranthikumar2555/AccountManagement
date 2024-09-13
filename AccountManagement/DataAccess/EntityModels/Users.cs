using System.ComponentModel.DataAnnotations;
using System.Data;

namespace AccountManagement.DataAccess.EntityModels
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public int? EmployeeId { get; set; }  // Nullable because not all users are employees (e.g., Admin)
        public Employee Employee { get; set; }
    }

}
