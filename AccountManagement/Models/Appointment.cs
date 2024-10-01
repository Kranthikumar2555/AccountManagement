using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }

        [Required(ErrorMessage = "Healthcare Professional Name is required.")]
        public string HealthcareProfessionalName { get; set; }

        [Required(ErrorMessage = "Appointment Date and Time is required.")]
        public DateTime AppointmentDateTime { get; set; }

        [Required(ErrorMessage = "Patient Name is required.")]
        public string PatientName { get; set; }

        [Required(ErrorMessage = "Patient Contact is required.")]
        public string PatientContact { get; set; }

        [Required(ErrorMessage = "Reason for Appointment is required.")]
        public string ReasonForAppointment { get; set; }
    }
}
