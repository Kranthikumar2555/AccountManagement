using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
namespace CourseManagement.Services
{
    public class GitHubSettings
    {
        public string? RepositoryOwner { get; set; }
        public string? RepositoryName { get; set; }
        public string? AccessToken { get; set; }
    }
    public class GitHubService:IGitHubService
    {
        private readonly HttpClient _httpClient;
        GitHubSettings _githubSettings;

        public GitHubService(HttpClient httpClient, IOptions<GitHubSettings> gitHubSettings)
        {
            _httpClient = httpClient;
            _githubSettings = gitHubSettings.Value;
        }

        public async Task CreateIssueAsync(string title, string body)
        {
            var url = $"https://api.github.com/repos/{_githubSettings.RepositoryOwner}/{_githubSettings.RepositoryName}/issues";

            var issue = new
            {
                title,
                body
            };

            var json = JsonSerializer.Serialize(issue);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("CourseManagementApp", "1.0"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _githubSettings.AccessToken);

            var response = await _httpClient.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"GitHub Issue creation failed: {errorResponse}");
            }
        }
    }
}
