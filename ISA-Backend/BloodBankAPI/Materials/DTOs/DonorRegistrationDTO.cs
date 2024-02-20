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
        [Required(ErrorMessage ="All fields need to be filled out!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        //Minimum eight characters, at least one uppercase letter, one lowercase letter and one number:
        [Required(ErrorMessage = "All fields need to be filled out!")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$",
            ErrorMessage ="Password needs to have at least eight characters, at least one uppercase and one lowercase letter and one number.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "All fields need to be filled out!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "All fields need to be filled out!")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "All fields need to be filled out!")]
        public string Address { get; set; }
        [Required(ErrorMessage = "All fields need to be filled out!")]
        public string City { get; set; }
        [Required(ErrorMessage = "All fields need to be filled out!")]
        public string State { get; set; }
        [Required(ErrorMessage = "All fields need to be filled out!")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "All fields need to be filled out!")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "All fields need to be filled out!")]
        public string JMBG { get; set; }
        [Required(ErrorMessage = "All fields need to be filled out!")]
        public string Workplace { get; set; }
        [Required(ErrorMessage = "All fields need to be filled out!")]
        public string Profession { get; set; }

        public DonorRegistrationDTO() { }
    }
}
