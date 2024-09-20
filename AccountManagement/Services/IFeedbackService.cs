using CourseManagement.Models;
using System.Threading.Tasks;

namespace CourseManagement.Services
{
    public interface IFeedbackService
    {
        Task AddFeedbackAsync(Feedback feedback);
    }
}
