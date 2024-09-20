namespace CourseManagement.Models
{
    /// <summary>
    /// Configuration settings for interacting with the GitHub API.
    /// </summary>
    public class GitHubSettings
    {
        /// <summary>
        /// Gets or sets the GitHub username or organization that owns the repository.
        /// </summary>
        public string? RepositoryOwner { get; set; }

        /// <summary>
        /// Gets or sets the name of the GitHub repository where issues will be created.
        /// </summary>
        public string? RepositoryName { get; set; }

        /// <summary>
        /// Gets or sets the GitHub access token used for authentication with the GitHub API.
        /// </summary>
        public string? AccessToken { get; set; }
    }
}
