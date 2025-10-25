using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clinicSystem.Models.Entities
{
    public class Patient: BaseModel
    {

        [Required (ErrorMessage = "Birth Date is required.")]

        public DateTime BirthDate { get; set; }

        [NotMapped]
        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - BirthDate.Year;

                // Subtract 1 if the birthday hasn't occurred yet this year
                if (BirthDate.Date > today.AddYears(-age))
                {
                    age--;
                }

                return age;
            }
        }

    }
}
