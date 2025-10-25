using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace clinicSystem.Models.Entities
{
    public class Clinic: BaseModel
    {

        [ValidateNever]
        public virtual List<Doctor> Doctors { get; set; }

    }
}
