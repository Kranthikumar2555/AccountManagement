using AccountManagement.DataAccess;
using AccountManagement.DataAccess.EntityModels;
using AccountManagement.Helpers;
using AccountManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AccountManagement.Controllers
{
    [Authorize(Roles = "Employee, Admin")]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult CreateOrUpdate(int? id, bool isAdmin)
        {
            var model = new EmployeeViewModel();
            var isEditing = id > 0;
            ViewData["IsEditing"] = isEditing;
            ViewData["isAdmin"] = isAdmin;

            if (isEditing)
            {
                var employee = _context.Employees.Find(id);
                if (employee == null)
                    return NotFound();

                model = new EmployeeViewModel
                {
                    EmployeeId = employee.EmployeeId,
                    Name = employee.Name,
                    Department = employee.Department,
                    JobTitle = employee.JobTitle,
                    Salary = employee.Salary,
                    RemoteWorkStatus = employee.RemoteWorkStatus
                };
            }
            else
            {
                var lastEmployee = _context.Employees.OrderByDescending(e => e.EmployeeId).FirstOrDefault();
                var newEmployeeId = lastEmployee != null ? lastEmployee.EmployeeId + 1 : 1;

                model = new EmployeeViewModel { EmployeeId = newEmployeeId };
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveEmployee([FromBody] Employee model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Invalid data provided." });

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var existingEmployee = await _context.Employees.FindAsync(model.EmployeeId);
                    if (existingEmployee != null)
                    {
                        var checkUserNameExist = _context.Employees.Where(x => x.Name == model.Name).Select(x => x.EmployeeId).FirstOrDefault();
                        if (checkUserNameExist == model.EmployeeId)
                        {
                            existingEmployee.Name = model.Name;
                            existingEmployee.Department = model.Department;
                            existingEmployee.JobTitle = model.JobTitle;
                            existingEmployee.Salary = model.Salary;
                            existingEmployee.RemoteWorkStatus = model.RemoteWorkStatus;

                            _context.Employees.Update(existingEmployee);
                            await _context.SaveChangesAsync();

                            var user1 = await _context.Users
                               .Include(u => u.Role)
                               .FirstOrDefaultAsync(u => u.EmployeeId == model.EmployeeId);
                            var user = new User
                            {
                                UserName = model.Name,
                                PasswordHash = user1.PasswordHash,
                                RoleId = 2,
                                EmployeeId = user1.EmployeeId
                            };
                            _context.Users.Update(user);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            return Json(new { success = false, message = "Employee Name Already Exists." });
                        }

                    }
                    else
                    {

                        var checkUserNameExist = _context.Employees.Any(x => x.Name == model.Name);
                        if (!checkUserNameExist)
                        {
                            var employee = new Employee
                            {
                                Name = model.Name,
                                Department = model.Department,
                                JobTitle = model.JobTitle,
                                Salary = model.Salary,
                                RemoteWorkStatus = model.RemoteWorkStatus
                            };
                            _context.Employees.Add(employee);
                            await _context.SaveChangesAsync();



                            var user = new User
                            {
                                UserName = model.Name,
                                PasswordHash = PasswordSecurityHelper.HashPassword("Password"),
                                RoleId = 2,
                                EmployeeId = employee.EmployeeId
                            };

                            _context.Users.Add(user);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            return Json(new { success = false, message = "Employee Name Already Exists." });
                        }

                    }

                    await transaction.CommitAsync();

                    return Json(new { success = true, message = "Employee saved successfully." });
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    return Json(new { success = false, message = "An error occurred while saving the employee." });
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> EmployeeDashboard(int employeeId)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);

            if (employee == null)
                return NotFound();

            return View(employee);
        }
    }
}
