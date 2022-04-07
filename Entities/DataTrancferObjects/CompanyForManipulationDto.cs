using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTrancferObjects
{
    public abstract class CompanyForManipulationDto
    {
        [Required(ErrorMessage = "Company ma,e is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the name is 60 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Company address is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for rhe Address is 60 characte.")]
        public string Address { get; set; }
        public string Country { get; set; }
        public IEnumerable<EmployeeForCreationDto> Employees { get; set; }

    }
}
