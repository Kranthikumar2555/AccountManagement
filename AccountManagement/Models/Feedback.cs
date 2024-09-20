using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagement.Models
{
    /// <summary>
    /// Represents a feedback entry submitted by a user.
    /// </summary>
    public class Feedback
    {
        /// <summary>
        /// Gets or sets the unique identifier for the feedback (Primary Key).
        /// </summary>
        [Key]
        public int FeedbackId { get; set; }  // Primary Key

        /// <summary>
        /// Gets or sets the customer's name.
        /// </summary>
        [Required(ErrorMessage = "Customer name is required.")]
        [StringLength(100, ErrorMessage = "Customer name cannot be longer than 100 characters.")]
        public string? CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the customer's email address.
        /// </summary>
        [Required(ErrorMessage = "Email Address is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(512, ErrorMessage = "Email address cannot be longer than 512 characters.")]
        public string? EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the type of feedback (e.g., Bug, Suggestion).
        /// </summary>
        [Required(ErrorMessage = "Feedback Type is required.")]
        [StringLength(50, ErrorMessage = "Feedback type cannot be longer than 50 characters.")]
        public string? FeedbackType { get; set; }

        /// <summary>
        /// Gets or sets the detailed feedback message provided by the user.
        /// </summary>
        [Required(ErrorMessage = "Feedback Message is required.")]
        [StringLength(1000, ErrorMessage = "Feedback message cannot be longer than 1000 characters.")]
        public string? FeedbackMessage { get; set; }

        /// <summary>
        /// Gets or sets the version of the app the feedback refers to.
        /// </summary>
        [Required(ErrorMessage = "App Version is required.")]
        [StringLength(20, ErrorMessage = "App version cannot be longer than 20 characters.")]
        [RegularExpression(@"^\d+(\.\d+){2}$", ErrorMessage = "App version must be in the format x.x.x (e.g., 1.0.0)")]
        public string? AppVersion { get; set; }

        /// <summary>
        /// Indicates whether the feedback should be integrated with GitHub (Not mapped to the database).
        /// </summary>
        [NotMapped]
        public bool IsIntegrateWithGitHub { get; set; }
    }
}
