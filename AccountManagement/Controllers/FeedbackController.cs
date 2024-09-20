using CourseManagement.Services;
using CourseManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Ganss.Xss;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.Controllers
{
    /// <summary>
    /// Controller to handle feedback submission and integration with GitHub.
    /// </summary>
    public class FeedbackController : Controller
    {
        private readonly IFeedbackService _feedbackService;
        private readonly IEncryptionService _encryptionService;
        private readonly IGitHubService _gitHubService;
        private readonly HtmlSanitizer _sanitizer;

        /// <summary>
        /// Constructor to inject required services for feedback management and GitHub integration.
        /// </summary>
        /// <param name="feedbackService">Service to handle feedback operations.</param>
        /// <param name="encryptionService">Service to handle email encryption.</param>
        /// <param name="gitHubService">Service to handle GitHub issue creation.</param>
        /// <param name="sanitizer">HTML sanitizer for input sanitization (default sanitizer is created here).</param>
        public FeedbackController(
            IFeedbackService feedbackService,
            IEncryptionService encryptionService,
            IGitHubService gitHubService,
            HtmlSanitizer sanitizer)
        {
            _feedbackService = feedbackService;
            _encryptionService = encryptionService;
            _gitHubService = gitHubService;
            _sanitizer = sanitizer ?? new HtmlSanitizer();  // Dependency injection ensures flexibility
        }

        /// <summary>
        /// Displays the feedback submission form.
        /// </summary>
        [HttpGet]
        public IActionResult SubmitFeedback()
        {
            return View();
        }

        /// <summary>
        /// Handles feedback form submission, processes it, and integrates with GitHub if required.
        /// </summary>
        /// <param name="feedback">Feedback object containing user input.</param>
        /// <returns>Returns the feedback form view with validation errors or redirects to success page.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitFeedback(Feedback feedback)
        {
            if (!ModelState.IsValid)
            {
                // Sanitize and display validation errors
                TempData["Message"] = _sanitizer.Sanitize(GetModelErrors(ModelState));
                return View(feedback);  // Return the same view to show validation errors
            }

            // Encrypt sensitive data
            feedback.EmailAddress = _encryptionService.EncryptEmail(feedback.EmailAddress);

            // Save feedback to the database
            await _feedbackService.AddFeedbackAsync(feedback);

            // Handle GitHub integration if requested
            if (feedback.IsIntegrateWithGitHub)
            {
                var issueTitle = $"Feedback from {feedback.CustomerName}";
                var issueBody = BuildIssueBody(feedback);

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

        /// <summary>
        /// Displays the success page after feedback is submitted.
        /// </summary>
        public IActionResult FeedbackSubmitted()
        {
            return View();
        }

        /// <summary>
        /// Retrieves and sanitizes validation error messages from the model state.
        /// </summary>
        /// <param name="modelState">The current model state.</param>
        /// <returns>A sanitized string of concatenated error messages.</returns>
        private string GetModelErrors(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(v => v.Errors);
            return string.Join("<br/>", errors.Select(e => e.ErrorMessage));
        }

        /// <summary>
        /// Builds a sanitized GitHub issue body from the feedback details.
        /// </summary>
        /// <param name="feedback">The feedback object containing user input.</param>
        /// <returns>A sanitized GitHub issue body string.</returns>
        private string BuildIssueBody(Feedback feedback)
        {
            return _sanitizer.Sanitize($@"
                **Feedback Type:** {feedback.FeedbackType}<br/>
                **App Version:** {feedback.AppVersion}<br/>
                **Feedback Message:**<br/>
                {feedback.FeedbackMessage}");
        }
    }
}
