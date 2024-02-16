using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodBankAPI.Materials.DTOs
{
    public class DonorRegistrationDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"")]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public long PhoneNumber { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public long JMBG { get; set; }
        [Required]
        public string Workplace { get; set; }
        [Required]
        public string Profession { get; set; }

        public DonorRegistrationDTO() { }
    }
}
