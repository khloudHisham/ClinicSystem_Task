using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace clinicSystem.Models.Entities
{
    public class Doctor: BaseModel
    {
        [Required(ErrorMessage = "Clinic is required.")]
        public int ClinicId { get; set; }

        [ValidateNever]
        public virtual Clinic Clinic { get; set; }

        [ValidateNever]
        public virtual List<Schedule> Schedules { get; set; }

    }
}
