using CourseManagement.Services;
using CourseManagement.Data;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHttpClient<IGitHubService, GitHubService>()
    .ConfigureHttpClient(client =>
    {
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Add("User-Agent", "AccountManagement"); // Required by GitHub API
    });
builder.Services.Configure<GitHubSettings>(builder.Configuration.GetSection("GitHub"));
builder.Services.AddScoped<IGitHubService, GitHubService>();
builder.Services.AddScoped<IFeedbackService, FeedbackService>();
builder.Services.AddScoped<IEncryptionService, EncryptionService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Feedback}/{action=SubmitFeedback}/{id?}");


app.Run();
