using CourseManagement.Data;
using CourseManagement.Models;
using System.Threading.Tasks;

namespace CourseManagement.Services
{
    /// <summary>
    /// Provides functionality for managing feedback-related operations.
    /// </summary>
    public class FeedbackService : IFeedbackService
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackService"/> class with a database context.
        /// </summary>
        /// <param name="context">The database context used to interact with the feedback data.</param>
        public FeedbackService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new feedback entry to the database.
        /// </summary>
        /// <param name="feedback">The feedback object containing the user's feedback information.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task AddFeedbackAsync(Feedback feedback)
        {
            // Add the feedback object to the DbSet for tracking and insertion
            _context.Feedbacks.Add(feedback);

            // Save changes asynchronously to the database
            await _context.SaveChangesAsync();
        }
    }
}
