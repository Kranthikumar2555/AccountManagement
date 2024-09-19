using System.ComponentModel.DataAnnotations;

namespace CourseManagement.Models
{
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }  // Primary Key

        [Required(ErrorMessage = "Customer name is required.")]
        [StringLength(100, ErrorMessage = "Customer name cannot be longer than 100 characters.")]
        public string? CustomerName { get; set; }

        [Required(ErrorMessage = "Email Address is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(512, ErrorMessage = "Email address cannot be longer than 150 characters.")]
        public string? EmailAddress { get; set; }

        [Required(ErrorMessage = "Feedback Type is required.")]
        [StringLength(50, ErrorMessage = "Feedback type cannot be longer than 50 characters.")]
        public string? FeedbackType { get; set; }

        [Required(ErrorMessage = "Feedback Message is required.")]
        [StringLength(1000, ErrorMessage = "Feedback message cannot be longer than 1000 characters.")]
        public string? FeedbackMessage { get; set; }

        [Required(ErrorMessage = "App Version is required.")]
        [StringLength(20, ErrorMessage = "App version cannot be longer than 20 characters.")]
        [RegularExpression(@"^\d+(\.\d+){2}$", ErrorMessage = "App version must be in the format x.x.x (e.g., 1.0.0)")]
        public string? AppVersion { get; set; }
    }
}
