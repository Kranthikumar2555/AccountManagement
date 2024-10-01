using Microsoft.EntityFrameworkCore;
using AccountManagement.Models;

namespace AccountManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>().HasData(
                new Appointment
                {
                    AppointmentId = 1,
                    HealthcareProfessionalName = "Dr. Smith",
                    AppointmentDateTime = new DateTime(2024, 10, 5, 14, 30, 0),
                    PatientName = "John Doe",
                    PatientContact = "1234567890",
                    ReasonForAppointment = "Annual check-up"
                },
                new Appointment
                {
                    AppointmentId = 2,
                    HealthcareProfessionalName = "Dr. Johnson",
                    AppointmentDateTime = new DateTime(2024, 10, 6, 9, 0, 0),
                    PatientName = "Jane Doe",
                    PatientContact = "9876543210",
                    ReasonForAppointment = "Flu symptoms"
                },
                new Appointment
                {
                    AppointmentId = 3,
                    HealthcareProfessionalName = "Dr. Adams",
                    AppointmentDateTime = new DateTime(2024, 10, 7, 11, 15, 0),
                    PatientName = "Michael Smith",
                    PatientContact = "5551234567",
                    ReasonForAppointment = "Follow-up visit"
                }
            );
        }
    }
}
