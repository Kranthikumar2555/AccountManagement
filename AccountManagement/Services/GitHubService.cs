using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CourseManagement.Models;

namespace CourseManagement.Services
{
    /// <summary>
    /// Provides functionality to interact with GitHub API for creating issues in a repository.
    /// </summary>
    public class GitHubService : IGitHubService
    {
        private readonly HttpClient _httpClient;
        private readonly GitHubSettings _githubSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="GitHubService"/> class with a configured HttpClient and GitHub settings.
        /// </summary>
        /// <param name="httpClient">The HttpClient instance used for sending HTTP requests.</param>
        /// <param name="gitHubSettings">The GitHub configuration options.</param>
        public GitHubService(HttpClient httpClient, IOptions<GitHubSettings> gitHubSettings)
        {
            _httpClient = httpClient;
            _githubSettings = gitHubSettings.Value;
        }

        /// <summary>
        /// Creates a new issue in the specified GitHub repository using the GitHub API.
        /// </summary>
        /// <param name="title">The title of the issue to be created.</param>
        /// <param name="body">The body (description) of the issue to be created.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task CreateIssueAsync(string title, string body)
        {
            // Build the GitHub API URL for creating an issue
            var url = $"https://api.github.com/repos/{_githubSettings.RepositoryOwner}/{_githubSettings.RepositoryName}/issues";

            // Create an object for the issue data
            var issue = new
            {
                title,
                body
            };

            // Serialize the issue data to JSON
            var json = JsonSerializer.Serialize(issue);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Set up HTTP request headers (GitHub API requires User-Agent and Authorization headers)
            _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("CourseManagementApp", "1.0"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _githubSettings.AccessToken);

            // Send the POST request to GitHub API
            var response = await _httpClient.PostAsync(url, content);

            // Check if the response indicates success, otherwise throw an exception with the error details
            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"GitHub Issue creation failed: {errorResponse}");
            }
        }
    }
}
