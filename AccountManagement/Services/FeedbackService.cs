using CourseManagement.Data;
using CourseManagement.Models;

namespace CourseManagement.Services
{
    public class FeedbackService
    {
        private readonly ApplicationDbContext _context;

        // Constructor to inject ApplicationDbContext
        public FeedbackService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Method to add feedback to the database
        public async Task AddFeedbackAsync(Feedback feedback)
        {
            // Add the feedback object to the DbSet
            _context.Feedbacks.Add(feedback);

            // Save changes asynchronously
            await _context.SaveChangesAsync();
        }
    }
}
