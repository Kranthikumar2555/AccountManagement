using CourseManagement.Services;
using CourseManagement.Data;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using CourseManagement.Models;
using Ganss.Xss;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// Register DbContext with SQL Server configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure GitHub service with HttpClient and default headers
builder.Services.AddHttpClient<IGitHubService, GitHubService>()
    .ConfigureHttpClient(client =>
    {
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Add("User-Agent", "AccountManagement"); // Required by GitHub API
    });

// Register other services
builder.Services.Configure<GitHubSettings>(builder.Configuration.GetSection("GitHub"));
builder.Services.AddScoped<IFeedbackService, FeedbackService>();
builder.Services.AddScoped<IEncryptionService, EncryptionService>();
builder.Services.AddScoped<HtmlSanitizer>();

var app = builder.Build();

// Middleware pipeline setup
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Define the default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Feedback}/{action=SubmitFeedback}/{id?}");

app.Run();
