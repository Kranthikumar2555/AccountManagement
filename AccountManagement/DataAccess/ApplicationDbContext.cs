using AccountManagement.DataAccess.EntityModels;
using AccountManagement.Helpers;
using AccountManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin", NormalizedName = "Admin Role", ConcurrencyStamp = new Guid().ToString() },
                new Role { Id = 2, Name = "Employee", NormalizedName = "Employee Role", ConcurrencyStamp = new Guid().ToString() }
            ); ;

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    UserName = "admin",
                    PasswordHash = PasswordSecurityHelper.HashPassword("Admin@123"),
                    RoleId = 1
                }
            );
        }



    }
}