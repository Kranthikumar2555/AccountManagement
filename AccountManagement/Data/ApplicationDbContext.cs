using CourseManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Data
{
    /// <summary>
    /// Represents the database context for the application, managing entity configurations and database interactions.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class with specified DbContext options.
        /// </summary>
        /// <param name="options">Options for configuring the DbContext, typically passed via dependency injection.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        /// <summary>
        /// Gets or sets the Feedbacks DbSet representing the Feedback table in the database.
        /// </summary>
        public DbSet<Feedback> Feedbacks { get; set; }  // The Feedback table

        /// <summary>
        /// Configures entity relationships and sets custom rules when creating the model.
        /// </summary>
        /// <param name="modelBuilder">The model builder used to configure the entity framework model.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Feedback entity with a primary key on FeedbackId
            modelBuilder.Entity<Feedback>().HasKey(f => f.FeedbackId);

            // Additional configurations can be placed here for other entities if necessary
        }
    }
}
