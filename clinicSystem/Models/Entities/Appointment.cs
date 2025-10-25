using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace clinicSystem.Models.Entities
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Patient is required.")]
        public int PatientId { get; set; }

        [ValidateNever]
        public virtual Patient Patient { get; set; }

        [Required(ErrorMessage = "Doctor is required.")]
        public int DoctorId { get; set; }

        [ValidateNever]
        public virtual Doctor Doctor { get; set; }

        [Required(ErrorMessage = "Appointment Date is required.")]
        public DateTime AppointmentDate { get; set; }

        [Required(ErrorMessage = "Appointment Time is required.")]
        public TimeSpan AppointmentTime { get; set; }

        [Required(ErrorMessage = "Duration is required.")]
        [Range(1, 60, ErrorMessage = "Duration must be between 1 and 60 minutes.")]
        public int Duration { get; set; } = 30;

    }
}
