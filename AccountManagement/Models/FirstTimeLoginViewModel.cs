namespace AccountManagement.Models
{
    using System.ComponentModel.DataAnnotations;


        public class FirstTimeLoginViewModel
        {

        public int EmployeeId { get; set; }

            [Required(ErrorMessage = "Password is required.")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required(ErrorMessage = "Confirm Password is required.")]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Passwords do not match.")]
            public string ConfirmPassword { get; set; }
        }
    

}
