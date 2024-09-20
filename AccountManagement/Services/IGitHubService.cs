namespace CourseManagement.Services
{
    public interface IGitHubService
    {
        Task CreateIssueAsync(string title, string body);
    }
}
