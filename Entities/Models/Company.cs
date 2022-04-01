using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Company
    {
        [Key]
        [Column("CompanyId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Company ma,e is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the name is 60 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Company address is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for rhe Address is 60 characte.")]
        public string Address { get; set; }

        public string Country { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
