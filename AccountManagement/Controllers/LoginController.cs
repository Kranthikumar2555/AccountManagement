using AccountManagement.Authentication;
using AccountManagement.DataAccess;
using AccountManagement.DataAccess.EntityModels;
using AccountManagement.Helpers;
using AccountManagement.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AccountManagement.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult AdminLogin()
        {
            ViewData["Role"] = "Admin";
            return View("LoginForm", new LoginViewModel { Role = "Admin" });
        }

        [HttpPost]
        public async Task<IActionResult> AdminLogin(LoginViewModel model)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserName == model.UserName && u.Role.Name == "Admin");

            if (user == null || !PasswordSecurityHelper.VerifyPassword(model.Password, user.PasswordHash))
            {
                ModelState.AddModelError("", "Invalid admin login attempt.");
                return View("LoginForm", model);
            }

            ClaimsIdentity identity = null;
            bool isAuthenticate = false;
                identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(ClaimTypes.Role,"Admin")
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                isAuthenticate = true;
 
            if (isAuthenticate)
            {
                var principal = new ClaimsPrincipal(identity);
                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("AdminDashboard", "Admin");
            }

            return View("LoginForm", model);


        }

        [HttpGet]
        public IActionResult EmployeeLogin()
        {
            ViewData["Role"] = "Employee";
            return View("LoginForm", new LoginViewModel { Role = "Employee" });
        }

        [HttpPost]
        public async Task<IActionResult> EmployeeLogin(LoginViewModel model)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserName == model.UserName && u.Role.Name == "Employee");

            if (user == null || !PasswordSecurityHelper.VerifyPassword(model.Password, user.PasswordHash))
            {
                ModelState.AddModelError("", "Invalid employee login attempt.");
                model.Role = "Employee";
                return  View("LoginForm", model);
            }

            var defaultPasswordHash = PasswordSecurityHelper.HashPassword("Password");
            if (user.PasswordHash == defaultPasswordHash)
            {
                return RedirectToAction("FirstTimeLoginRedirect", new { employeeId = user.EmployeeId });
            }

            ClaimsIdentity identity = null;
            bool isAuthenticate = false;
            identity = new ClaimsIdentity(new[]
            {
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(ClaimTypes.Role,"Employee"),
                    new Claim(ClaimTypes.Role,"Admin")
                }, CookieAuthenticationDefaults.AuthenticationScheme);
            isAuthenticate = true;

            if (isAuthenticate)
            {
                var principal = new ClaimsPrincipal(identity);
                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("EmployeeDashboard", "Employee", new { employeeId = user.EmployeeId });
            }
            ModelState.AddModelError("", "Invalid employee login attempt.");
            model.Role = "Employee";
            return View("LoginForm", model);

        }

        [HttpGet]
        public IActionResult FirstTimeLoginRedirect(int employeeId , string userName)
        {
            if(!string.IsNullOrEmpty(userName))
            {
                employeeId = _context.Employees.Where(x => x.Name == userName).Select(x => x.EmployeeId).FirstOrDefault();
            }

            return View(new FirstTimeLoginViewModel { EmployeeId = employeeId });
        }

       

        [HttpPost]
        public async Task<IActionResult> FirstTimeLogin1(FirstTimeLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
            }

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.EmployeeId == model.EmployeeId);

            if (user == null)
            {
                return Json(new { success = false, errors = new[] { "User not found." } });
            }

            user.PasswordHash = PasswordSecurityHelper.HashPassword(model.Password);
            _context.Users.Update(user);

            try
            {
                await _context.SaveChangesAsync();

                ClaimsIdentity identity = null;
                bool isAuthenticate = false;
                identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(ClaimTypes.Role,"Employee"),
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                isAuthenticate = true;

                if (isAuthenticate)
                {
                    var principal = new ClaimsPrincipal(identity);
                    var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    var role = (string)ViewData["Role"];
                    var redirectUrl = user.RoleId == 1 ? Url.Action("Index", "Employee")
                        : Url.Action("EmployeeDashboard", "Employee", new { employeeId = user.EmployeeId });

                    return Json(new { success = true, redirectUrl });
                }
            }
            catch (DbUpdateException)
            {
                return Json(new { success = false, errors = new[] { "An error occurred while updating the password. Please try again." } });
            }
            return Json(new { success = false, errors = new[] { "An error occurred while updating the password. Please try again." } });

        }

        [HttpGet]
        public async Task<IActionResult> Index(int? id)
        {
           
            return View("StartupView");
        }

        //public async Task DeleteUsersAndEmployeesAsync()
        //{
        //    // Delete users based on a condition
        //    var usersToDelete = await _context.Users
        //        .Where(u => u.UserId>1) // Replace with your actual condition
        //        .ToListAsync();

        //    if (usersToDelete.Any())
        //    {
        //        _context.Users.RemoveRange(usersToDelete); // Remove multiple users
        //    }

        //    // Delete employees based on a condition
        //    var employeesToDelete = await _context.Employees // Replace with your actual condition
        //        .ToListAsync();

        //    if (employeesToDelete.Any())
        //    {
        //        _context.Employees.RemoveRange(employeesToDelete); // Remove multiple employees
        //    }

        //    // Save changes to the database
        //    await _context.SaveChangesAsync();
        //}


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserName == model.UserName && u.Role.Name == model.Role);

            if (user == null || !PasswordSecurityHelper.VerifyPassword(model.Password, user.PasswordHash))
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }

            return RedirectToAction("Index", "Employee");
        }

        public IActionResult Logout(string role)
        {
            HttpContext.SignOutAsync();

            return View("LoginForm", new LoginViewModel { Role = role });
        }
    }
}
