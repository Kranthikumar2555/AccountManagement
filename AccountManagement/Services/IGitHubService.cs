namespace CourseManagement.Services
{
    /// <summary>
    /// Defines the contract for GitHub integration services.
    /// </summary>
    public interface IGitHubService
    {
        /// <summary>
        /// Creates a new issue in the specified GitHub repository.
        /// </summary>
        /// <param name="title">The title of the GitHub issue.</param>
        /// <param name="body">The body (description) of the GitHub issue.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task CreateIssueAsync(string title, string body);
    }
}
