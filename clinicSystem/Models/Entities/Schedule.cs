using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
namespace clinicSystem.Models.Entities
{
    public class Schedule
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Doctor is required.")]
        public int DoctorId { get; set; }

        [ValidateNever]
        public virtual Doctor Doctor { get; set; }

        [Required(ErrorMessage = "Day Of Work is required.")]
        public string DayOfWeek { get; set; } = string.Empty;

        [Required(ErrorMessage = "Start Time is required.")]
        public TimeSpan StartTime { get; set; }

        [Required(ErrorMessage = "End Time is required.")]
        public TimeSpan EndTime { get; set; }


    }
}
