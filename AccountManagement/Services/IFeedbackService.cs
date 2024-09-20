using CourseManagement.Models;
using System.Threading.Tasks;

namespace CourseManagement.Services
{
    /// <summary>
    /// Defines the contract for feedback-related operations.
    /// </summary>
    public interface IFeedbackService
    {
        /// <summary>
        /// Adds a new feedback entry to the system.
        /// </summary>
        /// <param name="feedback">The feedback object to be added.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddFeedbackAsync(Feedback feedback);
    }
}
