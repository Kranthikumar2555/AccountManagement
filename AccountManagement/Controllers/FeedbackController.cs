using CourseManagement.Models;
using CourseManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly FeedbackService _feedbackService;
        private readonly EncryptionService _encryptionService;

        // Constructor to inject FeedbackService
        public FeedbackController(FeedbackService feedbackService, EncryptionService encryptionService)
        {
            _feedbackService = feedbackService;
            _encryptionService = encryptionService;

        }
        [HttpGet]
        public IActionResult SubmitFeedback()
        {
            return View();
        }

        // Action to handle feedback form submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitFeedback(Feedback feedback)
        {

            if (!ModelState.IsValid)
            {
                // Check validation errors
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                TempData["Message"] = String.Join("<br/>",errors.Select(a=>a.ErrorMessage));
                return View(feedback);  // Return the same view to show validation errors
            }
            feedback.EmailAddress  = _encryptionService.EncryptEmail(feedback.EmailAddress);
            // If the model is valid, process feedback
            await _feedbackService.AddFeedbackAsync(feedback);
            return RedirectToAction("FeedbackSubmitted");
        }


        // Action to display a success message after feedback submission
        public IActionResult FeedbackSubmitted()
        {
            return View();
        }
    }
}
