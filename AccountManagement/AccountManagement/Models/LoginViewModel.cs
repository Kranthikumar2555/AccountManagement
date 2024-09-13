using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Models
{

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public string Role { get; set; } // "Admin" or "Employee"
    }

}
