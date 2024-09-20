using CourseManagement.Services;
using CourseManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Ganss.Xss;
namespace CourseManagement.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly IFeedbackService _feedbackService;
        private readonly IEncryptionService _encryptionService;
        private readonly IGitHubService _gitHubService;
        private HtmlSanitizer _sanitizer = new HtmlSanitizer();

        // Constructor to inject FeedbackService
        public FeedbackController(IFeedbackService feedbackService, IEncryptionService encryptionService, IGitHubService gitHubService)
        {
            _feedbackService = feedbackService;
            _encryptionService = encryptionService;
            _gitHubService = gitHubService;
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
                IEnumerable<ModelError> errors = ModelState.Values.SelectMany(v => v.Errors);
                TempData["Message"] = _sanitizer.Sanitize(String.Join("<br/>",errors.Select(a=>a.ErrorMessage)));
                return View(feedback);  // Return the same view to show validation errors
            }

            feedback.EmailAddress  = _encryptionService.EncryptEmail(feedback.EmailAddress);
            // If the model is valid, process feedback
            await _feedbackService.AddFeedbackAsync(feedback);
            if (feedback.IsIntegrateWithGitHub)
            {
                // Create a GitHub issue for this feedback
                var issueTitle = $"Feedback from {feedback.CustomerName}";
                var issueBody = _sanitizer.Sanitize($@"
                **Feedback Type:** {feedback.FeedbackType}
                **App Version:** {feedback.AppVersion}
                **Feedback Message:**
                {feedback.FeedbackMessage}");
                try
                {
                    await _gitHubService.CreateIssueAsync(issueTitle, issueBody);
                    TempData["GitHubIssueMessage"] = "GitHub issue created successfully!";
                }
                catch (HttpRequestException ex)
                {
                    TempData["GitHubIssueMessage"] = $"Error creating GitHub issue: {ex.Message}";
                }
            }
            return RedirectToAction("FeedbackSubmitted");
        }


        // Action to display a success message after feedback submission
        public IActionResult FeedbackSubmitted()
        {
            return View();
        }
    }
}
