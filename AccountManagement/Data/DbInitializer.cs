// Data/DbInitializer.cs
using AccountManagement.Models;
using System;
using System.Collections.Generic;
using AccountManagement.Data;

public static class DbInitializer
{
    public static void Seed(ApplicationDbContext context)
    {
        context.Database.EnsureCreated();

        // Check if there are any appointments already in the database
        if (context.Appointments.Any())
        {
            return; // Database has been seeded
        }

        var appointments = new List<Appointment>
        {
            new Appointment { HealthcareProfessionalName = "Dr. Smith", AppointmentDateTime = DateTime.Parse("2024-10-05 10:30:00"), PatientName = "John Doe", PatientContact = "1234567890", ReasonForAppointment = "Routine Checkup" },
            new Appointment { HealthcareProfessionalName = "Dr. Johnson", AppointmentDateTime = DateTime.Parse("2024-10-06 11:00:00"), PatientName = "Jane Doe", PatientContact = "0987654321", ReasonForAppointment = "Flu Symptoms" },
            new Appointment { HealthcareProfessionalName = "Dr. Brown", AppointmentDateTime = DateTime.Parse("2024-10-07 09:15:00"), PatientName = "Alice Smith", PatientContact = "5551234567", ReasonForAppointment = "Follow-up" },
            new Appointment { HealthcareProfessionalName = "Dr. Taylor", AppointmentDateTime = DateTime.Parse("2024-10-08 14:45:00"), PatientName = "Bob Johnson", PatientContact = "5559876543", ReasonForAppointment = "Consultation" },
            new Appointment { HealthcareProfessionalName = "Dr. Wilson", AppointmentDateTime = DateTime.Parse("2024-10-09 16:00:00"), PatientName = "Charlie Brown", PatientContact = "1231231234", ReasonForAppointment = "Checkup" },
            new Appointment { HealthcareProfessionalName = "Dr. Garcia", AppointmentDateTime = DateTime.Parse("2024-10-10 13:30:00"), PatientName = "David Wilson", PatientContact = "3213214321", ReasonForAppointment = "Vaccination" },
            new Appointment { HealthcareProfessionalName = "Dr. Martinez", AppointmentDateTime = DateTime.Parse("2024-10-11 15:00:00"), PatientName = "Eva Adams", PatientContact = "4564564567", ReasonForAppointment = "Dermatology" },
            new Appointment { HealthcareProfessionalName = "Dr. Clark", AppointmentDateTime = DateTime.Parse("2024-10-12 11:00:00"), PatientName = "Frankie White", PatientContact = "7897897890", ReasonForAppointment = "Dental Checkup" },
            new Appointment { HealthcareProfessionalName = "Dr. Lewis", AppointmentDateTime = DateTime.Parse("2024-10-13 10:15:00"), PatientName = "Grace Green", PatientContact = "2462462468", ReasonForAppointment = "Eye Examination" },
            new Appointment { HealthcareProfessionalName = "Dr. Lee", AppointmentDateTime = DateTime.Parse("2024-10-14 14:30:00"), PatientName = "Henry Black", PatientContact = "1351351357", ReasonForAppointment = "Nutrition Consultation" }
        };

        context.Appointments.AddRange(appointments);
        context.SaveChanges();
    }
}
